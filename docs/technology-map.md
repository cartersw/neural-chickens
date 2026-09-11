# Technology and implementation map

Researched 2026-09-11 for distinct 2D bodies, independently trained chickens and the existing Windows/.NET/SQL/Next.js stack. Recommendations remain subject to compatibility/performance proofs. See the [pipeline](simulation-pipeline.md) for flow and completion criteria, and [TASKS.md](../TASKS.md) for milestones.

**Paths are proposed unless already present.** Aliases:

| Alias | Repository location |
|---|---|
| `U` | `simulator/NeuralChickensSimulator` |
| `API` | `backend/NeuralChickens.Api` |
| `App` | `backend/NeuralChickens.Api.Application` |
| `Domain` | `backend/NeuralChickens.Api.Domain` |
| `Worker` | `backend/NeuralChickens.Worker` |
| `Web` | `web/src` |

Models/video belong under a configured artifact root outside tracked source. Credentials use secret/environment facilities.

## Stage 0 — Reproducible foundation

| Choice | Placement |
|---|---|
| Retain pinned Unity Editor/packages | `U/ProjectSettings/ProjectVersion.txt`, `U/Packages/` |
| Conda/Mamba; lock Conda packages **and pip wheels** | `simulator/python/{environment.yml,locks/,README.md}` |
| Retain .NET SDK/NuGet and npm | Root `global.json`, backend projects, `web/package.json` and lockfile |
| Versioned JSON; System.Text.Json and simple Unity JsonUtility DTOs | `simulator/contracts/`, `simulator/config/`, `App/DTOs/`, `U/Assets/Scripts/Configuration/` |
| EF migrations; immutable artifact keys/SHA-256 manifests | `Domain/Migrations/`, `docs/operations/data-migration.md`, artifact root |

Inventory: Unity **6000.5.9f1**, ML-Agents **4.1.0**, Inference Engine **2.6.1**. Unity documents Python **3.10.12**, Python `mlagents==1.1.0` and a PyTorch **2.2.1-compatible** Windows setup; repository commands specify torch **2.1.1**. Prove clean recreation, dependency resolution, handshake, export and inference before locking the combination. [Unity installation](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.1/manual/Installation.html)

Conda exports alone do not lock pip wheels. `uv` is optional if the exact environment reproduces. JsonUtility's limitations may justify Newtonsoft.Json for dictionaries/polymorphism. [Conda](https://docs.conda.io/projects/conda/en/stable/user-guide/tasks/manage-environments.html), [uv](https://docs.astral.sh/uv/concepts/resolution/), [JSON limits](https://docs.unity3d.com/6000.0/Documentation/Manual/json-serialization.html)

## Stages 1–2 — Bodies and independent learning

| Choice | Placement |
|---|---|
| Physics2D/ML-Agents; explicit resets/outcomes, normalized observations, bounded controls | `U/Assets/Scripts/{Environments/Find,Agents,Configuration}/` |
| Rigidbody2D/Collider2D/HingeJoint2D motors; bounded rig schemas | `U/Assets/Scripts/Bodies/`, `U/Assets/Prefabs/Chickens/`, `simulator/contracts/body/` |
| PPO/PyTorch; independent initialization/checkpoints per chicken | `simulator/config/trainers/{find-ppo,locomotion-ppo}.yaml`; artifacts `training/<chicken>/<body>/<run>/` |
| TensorBoard, structured results, Unity Profiler/Test Framework | `simulator/evaluation/`, `U/Assets/Tests/{EditMode,PlayMode}/`, run artifacts |

Parallel arenas collect experience for **one chicken's policy**. Prove two different bodies learn independently; measure CPU/GPU, arena count, memory and seed variation. Compare SAC when experience collection is the bottleneck: replay reuse trades against update cost and memory. [Trainer configuration](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.1/manual/Training-Configuration-File.html), [joints](https://docs.unity3d.com/6000.0/Documentation/Manual/2d-physics/joints/hinge-joint-2d-reference.html), [TensorBoard](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.1/manual/Using-Tensorboard.html), [Profiler](https://docs.unity3d.com/6000.0/Documentation/Manual/profiler-introduction.html)

## Stage 3 — Evaluation and model delivery

| Choice | Placement |
|---|---|
| Frozen evaluator sharing gameplay code; raw outcomes/reports | `U/Assets/Scripts/Evaluation/`, `simulator/config/evaluation/`, `simulator/evaluation/` |
| **Prove:** ONNX import → ModelAsset → platform AssetBundle → runtime binding | `U/Assets/Editor/ModelPackaging/`, `U/Assets/Scripts/Inference/`; isolated packaging workspace |
| Training/evaluation/rendered builds; compatibility manifests | `U/Assets/Editor/Builds/`, `simulator/contracts/artifacts/` |

`Agent.SetModel` expects **ModelAsset**; serialized `.sentis` loading returns **Model**. AssetBundle packaging remains unproven. One unchanged player must simultaneously run both correct rigs/policies, including post-build models. Check schema/platform compatibility, action parity, disposal and headless inference. ONNX Runtime/direct Inference Engine fallbacks require observation/action/metadata adapters and runtime deployment checks. [AssetBundles](https://docs.unity3d.com/6000.0/Documentation/Manual/AssetBundlesIntro.html), [Agent API](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.1/api/Unity.MLAgents.Agent.html), [serialized Model](https://docs.unity3d.com/Packages/com.unity.ai.inference@2.6/manual/serialize-a-model.html), [ONNX Runtime](https://onnxruntime.ai/docs/get-started/with-csharp.html)

## Stage 4 — Durable training product

| Choice | Placement |
|---|---|
| ASP.NET Core 10/EF/SQL; semantic validation/Problem Details | `API/Controllers/`, `App/{Services,DTOs}/`, `Domain/{Entities,Configurations,Migrations}/` |
| **Hangfire Core + SqlServer**, separate Worker; one compute queue/WorkerCount=1 | `Worker/{Jobs,Dispatch}/`; Domain jobs/attempts/outbox; Hangfire SQL schema |
| Process/ArgumentList, asynchronous output, cancellation/artifact publication | `Worker/{Execution,Artifacts}/`, `App/Interfaces/` |
| ASP.NET OpenAPI + openapi-typescript/openapi-fetch | `API/Program.cs`, `Web/generated/api.d.ts`, `Web/services/simulationService.ts` |
| Next/React/Tailwind, native forms + Zod, fetch/SWR | `Web/app/{chickens,simulations,api}/`, `Web/{components,hooks,lib/validation}/` |
| React SVG preview; Unity validates physics | `Web/components/chickens/BodyPreview.tsx` |
| xUnit, WebApplicationFactory, Testcontainers.MsSql, Playwright | `backend/{NeuralChickens.Api.Tests,NeuralChickens.Worker.Tests}/`, `web/tests/e2e/`, `.github/workflows/` |

**Hangfire:** save request/job/outbox together; dispatch ID-only invocations. Crashes can duplicate delivery. Domain attempts fence completion and keep execution/publication idempotent. Start with zero exception retries; add bounded transient retries later. This does **not** prevent crash redelivery. Prove .NET/SqlClient compatibility, long-job ownership, outages/restarts and process reconciliation. Hangfire replaces scheduling, not child-process supervision: keep handlers active until owned work ends. Never silently restart an interrupted observed event. [Hosting](https://docs.hangfire.io/en/latest/background-processing/processing-jobs-in-console-app.html), [SQL](https://docs.hangfire.io/en/latest/configuration/using-sql-server.html), [retries](https://docs.hangfire.io/en/latest/background-processing/dealing-with-exceptions.html), [cancellation](https://docs.hangfire.io/en/latest/background-methods/using-cancellation-tokens.html), [concurrency limits](https://docs.hangfire.io/en/latest/background-processing/throttling.html)

Read local Next 16 docs before coding. Generated types do not validate JSON. SWR handles polling; consider TanStack Query only for richer mutation/cache needs. [Next](https://nextjs.org/docs/app/getting-started/fetching-data), [OpenAPI TypeScript](https://openapi-ts.dev/introduction), [Zod](https://zod.dev/)

Test SQL semantics against pinned SQL Server via Testcontainers/Docker Linux containers or an isolated database—not SQLite/EF InMemory. GPU/mobile video needs separate device checks. [ASP.NET tests](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-10.0), [Testcontainers](https://dotnet.testcontainers.org/modules/mssql/), [Playwright](https://playwright.dev/docs/intro)

## Stage 5 — Events and history

| Choice | Placement |
|---|---|
| Unity event clock, isolated initial lanes, independently frozen policies; no multiplayer framework | `U/Assets/Scripts/Events/`, `U/Assets/Scripts/Environments/Find/`, `Worker/Jobs/` |
| SQL/EF event snapshots/results; existing dispatch safeguards | `Domain/Entities/`, `App/Services/`, `API/Controllers/` |
| Next/SWR history/results | `Web/app/events/[id]/`, `Web/app/chickens/[id]/` |

Unity computes outcomes; .NET persists history. Viewing starts nothing. Rematches get new event IDs.

## Stage 6 — Recording and local viewing

| Choice | Placement |
|---|---|
| **Prove:** RenderTexture/AsyncGPUReadback → FFmpeg/H.264 | `U/Assets/Scripts/Presentation/`, `Worker/Media/`, `deployment/local/encoding/` |
| Pinned MediaMTX; fMP4 recording, finalized MP4/playback service | `deployment/local/mediamtx.yml`, media directory, Domain media records |
| HTML video + hls.js/native HLS | `Web/components/media/VideoPlayer.tsx`, event page/API metadata |
| Idempotent hooks/periodic reconciliation | `Worker/{Media,Artifacts}/`, App media service/API hook |

Prove synthetic then Unity ingest: colors, orientation, pacing, bounded buffers, resource use, seeking/finalization. Desktop capture is a prototype fallback; Unity WebRTC/WHIP requires codec/recording/reconnect tests. [Readback](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Rendering.AsyncGPUReadback.html), [FFmpeg capture](https://ffmpeg.org/ffmpeg-devices.html#gdigrab), [Unity publishing](https://mediamtx.org/docs/publish/unity)

MediaMTX does not create adaptive bitrate ladders. Validate one local rendition. NVENC needs supported hardware/drivers/build; record FFmpeg build/licensing if distributed. [Recording](https://mediamtx.org/docs/features/record), [playback](https://mediamtx.org/docs/features/playback), [NVENC](https://docs.nvidia.com/video-technologies/video-codec-sdk/13.1/ffmpeg-with-nvidia-gpu/index.html), [FFmpeg license](https://ffmpeg.org/doxygen/trunk/md_LICENSE.html)

## Stage 7 — Public access and operation

| Choice | Placement |
|---|---|
| **Prove Auth0** Universal Login/Next SDK; Next BFF; ASP.NET JwtBearer | `Web/lib/auth0.ts`, `Web/proxy.ts`, `Web/app/api/`, `API/Authorization/`, Domain issuer/subject mapping |
| ASP.NET rate limits/durable quotas; Vercel/cloudflared REST | API/App, `deployment/windows/cloudflared/`, Vercel settings |
| **Preferred candidate: Cloudflare Stream** SRT/RTMPS, adaptive live/replay; retain local recording | App media-provider interface, `Worker/Media/CloudflareStream/`, API hooks, `deployment/cloudflare/` |
| Alternative: public MediaMTX + Caddy/VPS | `deployment/public/{mediamtx.yml,Caddyfile}`, service definitions |
| Windows Services; prove rendering context/interactive scheduled task | `deployment/windows/`, Worker capabilities/ownership |
| ILogger/ActivitySource/Meter + OpenTelemetry/OTLP | API/Worker Diagnostics, `Web/instrumentation.ts`, `deployment/observability/` |
| SQL/artifact backups, restore/rollback; ordinary CI/isolated GPU checks | `deployment/backup/`, `docs/operations/`, `.github/workflows/` |
| Product guidance | `Web/app/about/`, help components |

**Auth:** accept provider plan first. Use Next HttpOnly sessions/server-acquired audience-scoped tokens; disable an unnecessary browser token endpoint. Test CSRF/origins, expiry, issuer/audience and API ownership. Self-managed Identity/OIDC is the alternative; simple Identity bearer tokens are not an OIDC/JWT issuer. [Auth0 SDK](https://github.com/auth0/nextjs-auth0), [plans](https://auth0.com/pricing), [JWT validation](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/configure-jwt-bearer-authentication?view=aspnetcore-10.0), [Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-10.0)

**Video:** estimate live/replay viewer-minutes, retention/buffering; approve a bounded test budget **before provisioning**, then validate ingest, latency, readiness and event IDs **before launch**. Recheck pricing. Stream WHIP/WHEP currently lacks recording/HLS bridging, hence SRT/RTMPS. Choose public MediaMTX when control, portability or measured total cost wins. [Ingest](https://developers.cloudflare.com/stream/stream-live/start-stream-live/), [playback](https://developers.cloudflare.com/stream/viewing-videos/using-own-player/), [WebRTC limits](https://developers.cloudflare.com/stream/webrtc-beta/#limitations), [pricing](https://developers.cloudflare.com/stream/pricing/)

Tunnel is for REST; standard proxy/tunnel service is not an unrestricted video CDN. VPS media needs direct/DNS-only routing, TLS and private management ports. [Cloudflare policy](https://developers.cloudflare.com/fundamentals/reference/policies-compliances/delivering-videos-with-cloudflare/), [Caddy HTTPS](https://caddyserver.com/docs/automatic-https)

**Windows:** Services run in session 0; interactive tasks need a logged-on user. Test rendering through lock/disconnect/logout/reboot; unavailable graphics leaves jobs queued. [Services](https://learn.microsoft.com/en-us/windows/win32/services/interactive-services), [task logon](https://learn.microsoft.com/en-us/windows/win32/taskschd/principal-logontype)

**Telemetry:** propagate trace context through jobs; IDs belong in logs/traces, bounded dimensions in metrics. Optional local Aspire dashboard; Grafana Cloud when retention/budget justifies it. [OpenTelemetry](https://learn.microsoft.com/en-us/dotnet/core/diagnostics/observability-with-otel), [Aspire](https://aspire.dev/dashboard/standalone/), [Grafana OTLP](https://grafana.com/docs/grafana-cloud/send-data/otlp/send-data-otlp/)

## Stages 8–9 — Gameplay and measured expansion

| Choice | Placement / adoption condition |
|---|---|
| Race/Balance/Sumo; richer bounded rigs | `U/Assets/Scripts/Environments/{Race,Balance,Sumo}/`, Bodies/Prefabs, trainer configs, App/Domain mode detail, web creator |
| Independent Sumo learner against frozen opponents; prove self-play/schema fit | Agent adapters, `simulator/config/evaluation/opponents/`; separate chicken checkpoints |
| NumPy/pandas, SciPy intervals/resampling, Matplotlib | `simulator/evaluation/analysis/`, **separate locked analysis environment**; API summaries |
| Profiler/trainer timings/dotnet-counters/GPU metrics before more workers | `deployment/benchmarks/`, Worker capacity; WorkerCount is per host, not global |
| R2/S3-compatible client; S3 if native versioning required | App storage interface, `Worker/Artifacts/`, `deployment/storage/`; prove upload/hash/retention/restore/access |
| MLflow when searchable experiments outgrow manifests/TensorBoard | `simulator/tools/tracking/`; SQL retains product state |
| Alternative broker/scheduler, WebRTC, state replay, morphology search | Existing boundaries/isolated experiments after a documented unmet requirement |

Sumo needs held-out opponents and heterogeneous-policy checks. Analyze repeated frozen events with sample sizes/uncertainty; correlated chicken outcomes are not independent samples. Training budgets do not set calibrated probabilities. [Self-play](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.1/manual/Training-Configuration-File.html#self-play), [SciPy intervals](https://docs.scipy.org/doc/scipy/reference/generated/scipy.stats._result_classes.BinomTestResult.proportion_ci.html), [bootstrap](https://docs.scipy.org/doc/scipy/reference/generated/scipy.stats.bootstrap.html), [MLflow](https://mlflow.org/docs/latest/ml/tracking/)

Object stores do not replace live transcoding. R2 presigned browser access needs its S3 endpoint/CORS; configure backup retention/recovery explicitly. [R2 links](https://developers.cloudflare.com/r2/api/s3/presigned-urls/), [CORS](https://developers.cloudflare.com/r2/buckets/cors/), [S3 versioning](https://docs.aws.amazon.com/AmazonS3/latest/userguide/Versioning.html)

## Engine/framework alternatives

Retain Unity/ML-Agents unless a measured requirement justifies rebuilding integration:

| Alternative | Revisit when |
|---|---|
| Godot RL Agents | Open-source engine requirements dominate; ONNX deployment still needs proof. [Project](https://github.com/edbeeching/godot_rl_agents) |
| Pymunk + Gymnasium + SB3 | Python-first research outweighs rebuilding bodies/presentation. Avoid unvalidated training/event physics transfer. [Pymunk](https://www.pymunk.org/en/latest/), [SB3](https://stable-baselines3.readthedocs.io/en/master/guide/custom_env.html) |
| MuJoCo | Demonstrated articulated-contact limits justify changing physics. [Overview](https://mujoco.readthedocs.io/en/stable/overview.html) |
| Custom SQL scheduler / Quartz | Hangfire fails ownership/recovery gates / needs unsupported calendar semantics. [Quartz](https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/job-stores.html) |

No current evidence justifies rewriting ASP.NET/EF/SQL/Next.js or adding another ORM, multiplayer framework or distributed RL platform.
