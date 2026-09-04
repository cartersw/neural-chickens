# Simulation Processing Pipeline

Design doc for the system that takes `Simulation` rows in `Requested` state, trains an
ML-Agents brain for them in Unity, and later plays them back as a live video stream.

## Target architecture

```mermaid
flowchart TB
    subgraph vercel [Vercel]
        Web["Next.js 16 / React 19"]
    end
    subgraph home [Self-hosted Windows box, NVIDIA GPU]
        Api["NeuralChickens.Api<br/>ASP.NET Core 10"]
        Db[("SQL Server<br/>Docker")]
        Worker["NeuralChickens.Worker<br/>BackgroundService"]
        Trainer["Unity headless build<br/>+ mlagents-learn"]
        Player["Unity playback build<br/>runs trained brain"]
        FF["FFmpeg h264_nvenc"]
        MTX["MediaMTX<br/>record + LL-HLS + WHEP"]
    end
    subgraph edge [Ingress]
        CFT["Cloudflare Tunnel<br/>JSON API only"]
        VPS["VPS relay<br/>video only"]
    end

    Web -->|REST| CFT --> Api
    Api --> Db
    Worker --> Db
    Worker -->|spawn| Trainer
    Worker -->|spawn| Player
    Trainer -->|".onnx"| Worker
    Player -->|raw frames| FF -->|SRT| MTX
    MTX -->|webhooks| Api
    MTX -->|"SRT relay"| VPS
    VPS -->|LL-HLS| Web
```

**Why the ingress is split:** Cloudflare Tunnel is fine for the JSON API, but it cannot carry
WebRTC (no UDP for public hostnames) and Cloudflare's
[Service-Specific Terms](https://www.cloudflare.com/service-specific-terms-application-services/)
prohibit serving video over the CDN on Free/Pro/Business plans. Video needs either a ~$5/mo VPS
relay or a direct port-forward. Test for CGNAT before assuming port-forwarding is available.

## Key decisions

- **Worker orchestration in .NET.** A new `NeuralChickens.Worker` project referencing
  `NeuralChickens.Api.Domain` so it shares `NeuralChickensDbContext`. Direct DB access rather than
  HTTP — simplest for a solo project; the coupling is acceptable because both are our code and
  deploy together.
- **Streaming via FFmpeg + MediaMTX, not Unity Render Streaming.** Render Streaming's last
  functional release was Dec 2024, it officially supports only Unity 2020–2023, its
  `com.unity.webrtc` dependency was deprecated in Aug 2026, and it does one encode *per viewer*
  (~5 max). FFmpeg + MediaMTX encodes once and fans out to unlimited viewers while recording to
  disk in the same pass. NVENC is a dedicated ASIC separate from the CUDA cores training uses, so
  streaming costs almost no training throughput.
- **Separate Unity builds for training and playback.** Training is headless; playback renders and
  streams. Different requirements, different build targets.

## Phase 0 — De-risk the foundations

Do this before writing any worker code; each item is a known trap.

- **Commit `Packages/manifest.json`.** [simulator/NeuralChickensSimulator/](../simulator/NeuralChickensSimulator/)
  has no `Packages/` folder tracked at all, so the ML-Agents and URP versions aren't pinned
  anywhere. This is the biggest reproducibility hole right now.
- **Pin versions deliberately.** Verify the Python/C# pairing on the
  [ML-Agents README version table](https://github.com/Unity-Technologies/ml-agents) and pin both
  sides. See [Open question](#open-question).
- **Python version.** `mlagents` is strict about Python (3.10.x for 1.1.0). Set up a dedicated venv
  on the server.
- **Write a trainer YAML.** None exists in the repo. Create `simulator/config/find.yaml` with a PPO
  block and get `mlagents-learn` training against the Editor first, then against a build.
- **Spike the runtime-brain problem.** Unity's Inference Engine cannot load `.onnx` at runtime —
  ONNX→`.sentis` conversion is Editor-only, and `BehaviorParameters.SetModel` wants a `ModelAsset`,
  not a `Model`. Since a new brain is generated per simulation, resolve this early. Three candidate
  routes, in order of preference:
  1. Worker invokes `Unity.exe -batchmode -quit -executeMethod ModelPacker.Serialize` post-training
     to emit a `.sentis`, then playback loads it from disk with `ModelLoader.Load(path)` and drives
     inference manually. The net is 6 observations → 2 continuous actions, so hand-rolling the
     inference loop is genuinely small.
  2. [onnxruntime-unity](https://github.com/asus4/onnxruntime-unity) to load `.onnx` directly at
     runtime, bypassing ML-Agents inference.
  3. Bake models as assets — rejected, models are dynamic.

## Phase 1 — Training pipeline (first milestone)

### Schema changes

`SimulationStatus` currently stops at `Completed` and has no failure path. Extend it:

```csharp
public enum SimulationStatus
{
    Requested = 0, Claimed = 1, Training = 2, Trained = 3,
    Failed = 4, Cancelled = 5
}
```

Add training artifact fields to
[Simulation.cs](../backend/NeuralChickens.Api.Domain/Entities/Simulation.cs): `RunId`, `ModelPath`,
`FinalReward`, `StepsTrained`, `ClaimedAt`, `LeaseExpiresAt`, `FailureReason`. Drop `VideoPath` — it
belongs on a broadcast, not a training job.

Add a **`SimulationBroadcast`** entity. A trained simulation can be played live many times, so
playback needs its own lifecycle: `Id`, `SimulationId`, `StreamKey`, `StartedAt`, `EndedAt`,
`RecordingPath`, `WinnerChickenId`. This is what "the live demonstration is saved in the database"
actually means.

### Job claiming

One worker today, but write the claim correctly from the start so a crashed worker doesn't strand
jobs:

```sql
UPDATE TOP (1) s WITH (ROWLOCK, READPAST)
SET Status = 1, WorkerId = @workerId, LeaseExpiresAt = DATEADD(minute, 5, SYSUTCDATETIME())
OUTPUT INSERTED.Id
FROM Simulations s
WHERE Status = 0 OR (Status IN (1,2) AND LeaseExpiresAt < SYSUTCDATETIME())
```

Heartbeat the lease from the worker while training runs.

### Parameterizing Unity per run

[MoveToGoalAgent.cs](../simulator/NeuralChickensSimulator/Assets/Scripts/MoveToGoalAgent.cs)
hardcodes everything (`moveSpeed = 2f`, reset to `Vector3.zero`). Two mechanisms, use both:

- **Structural config** (contestants, arena, seed): worker writes `run-config.json`, passes it
  through `mlagents-learn --env-args --sim-config <path>`, Unity reads it in `Awake()` via
  `Environment.GetCommandLineArgs()`.
- **Scalar tunables** (speed, reward weights): the `environment_parameters` block in the trainer
  YAML, read via `Academy.Instance.EnvironmentParameters.GetWithDefault("speed", 3f)`. This is also
  the mechanism reused for odds shaping in Phase 5, so wiring it now pays off twice.

### Worker loop

`Poll → claim → generate YAML + run-config → spawn mlagents-learn → stream stdout to logs → enforce
wall-clock cap → parse results/<run-id>/ → copy .onnx → update status`.

The wall-clock cap ("trained past a certain time limit") is a `CancellationTokenSource` timeout that
kills the process tree — `mlagents-learn` caps on `max_steps`, not time, so time-capping is our job.

### API surface

Replace the stubs in
[SimulationService.cs](../backend/NeuralChickens.Api.Application/Services/SimulationService.cs) —
`GetSimulationAsync` currently returns hardcoded data. Add list/filter, cancel, and a real status
read. Note `GetSimulationDto` exposes a `CreatedAt` that doesn't exist on the entity.

## Phase 2 — Playback build (offline before live)

Get a headless playback run producing a correct result and a local MP4 *before* introducing
streaming. A second Unity build target that loads the trained brain, runs N contestants, records the
winner, and writes a `SimulationBroadcast` row.

## Phase 3 — Streaming

Build bottom-up, isolating the streaming stack from the capture stack:

1. **FFmpeg `testsrc` → MediaMTX → hls.js in the browser.** No Unity involved. Prove the pipe first.
2. **Unity `AsyncGPUReadback` → FFmpeg stdin.** Render to a `RenderTexture`, keep 2–3 readbacks in
   flight so the render thread never stalls, drop frames rather than block.
3. **MediaMTX recording + webhooks** back to the API.
4. **Player component** in Next.js.

Five traps worth pre-empting:

- `-bf 0` on NVENC, or WebRTC silently shows black — browsers reject H.264 B-frames.
- `recordDeleteAfter: 0s`, or recordings vanish after 24h.
- The hook is `runOnAvailable`, not `runOnReady` — renamed in MediaMTX v1.19.3, so every tutorial
  is stale.
- `-preset llhq` no longer exists; it's `-preset p4 -tune ull`.
- Unity's readback is bottom-up, so FFmpeg needs `vflip`.

`hls.js` must be dynamically imported inside `useEffect` — it touches `window` at module scope and
will break SSR. Per [web/AGENTS.md](../web/AGENTS.md), check `node_modules/next/dist/docs/` before
writing Next.js code; this is Next 16 with breaking changes.

## Phase 4 — Public exposure

Cloudflare Tunnel for the API, VPS relay or port-forward for video, short-lived JWTs minted by the
API for stream authorization (MediaMTX supports `authMethod: jwt` with a JWKS endpoint).

## Phase 5 — Odds shaping

Per-contestant `environment_parameters` and asymmetric training budgets. Phase 1's parameter
plumbing is the prerequisite.

## What to learn

Roughly in the order it comes up:

- .NET `BackgroundService`, and `System.Diagnostics.Process` with redirected stdout, cancellation,
  and process-tree kill
- SQL job-queue patterns: claim/lease/heartbeat, `READPAST`
- ML-Agents trainer YAML and PPO hyperparameters, `mlagents-learn` CLI, `EnvironmentParameters`,
  side channels
- Unity `BuildPipeline` Editor scripts driven by `-batchmode -executeMethod`
- Unity Inference Engine model loading, `AsyncGPUReadback`, `RenderTexture`
- FFmpeg rawvideo input, NVENC low-latency flags, SRT
- MediaMTX config, hooks, control API
- HLS/LL-HLS/WebRTC fundamentals and `hls.js`
- Practical networking: NAT, CGNAT, port forwarding, TLS, reverse proxying

## Open question

Whether to stay on `mlagents==1.1.0` / package 4.0.0 or move to 4.1.0. Resolve this as the first
Phase 0 task by reading the version table on the
[ML-Agents README](https://github.com/Unity-Technologies/ml-agents) — it maps each release to its
exact Python `mlagents` version and C# package version. Release 23 (Aug 2025) pairs `mlagents`
1.1.0 with package 4.0.0, which is what
[requirements.txt](../simulator/NeuralChickensSimulator/requirements.txt) currently targets; 4.1.0
shipped July 2026 and needs its Python counterpart confirmed rather than assumed. Nail the pairing
down before building anything, since a mismatch surfaces as confusing gRPC handshake failures at
training time rather than a clean error.

## Checklist

### Phase 0 — Foundations
- [ ] Commit `Packages/manifest.json`; pin `com.unity.ml-agents` and the matching Python `mlagents`
- [ ] Set up a Python 3.10 venv on the server
- [ ] Write `simulator/config/find.yaml` and train against the Editor, then a headless build
- [ ] SPIKE: resolve runtime ONNX loading (`.sentis` batchmode serialization, or onnxruntime-unity)

### Phase 1 — Training pipeline
- [ ] Extend `SimulationStatus`, add training artifact fields, add `SimulationBroadcast`, drop `VideoPath`, migrate
- [ ] Create `NeuralChickens.Worker` BackgroundService with claim/lease/heartbeat polling
- [ ] Parameterize the Unity agent via `run-config.json` + `EnvironmentParameters`
- [ ] Editor `BuildPipeline` script for the headless training build
- [ ] Worker spawns `mlagents-learn`, streams logs, enforces wall-clock cap, parses results, copies `.onnx`
- [ ] Replace `SimulationService` stubs; add list/filter/cancel; fix the `CreatedAt` mismatch

### Phase 2 — Playback
- [ ] Unity playback build: load trained brain, run contestants headless, record winner

### Phase 3 — Streaming
- [ ] Prove FFmpeg `testsrc` → MediaMTX → hls.js with no Unity involved
- [ ] Unity `AsyncGPUReadback` → FFmpeg stdin → SRT
- [ ] MediaMTX recording + webhooks into the API
- [ ] Next.js player component and simulation status UI

### Phase 4 — Exposure
- [ ] Cloudflare Tunnel for the API, VPS relay or port-forward for video, JWT stream auth

### Phase 5 — Odds shaping
- [ ] Per-contestant `environment_parameters` and asymmetric training budgets
