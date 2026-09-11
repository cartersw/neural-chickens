# Development backlog

Replanned 2026-09-11 against `b389044`. [Pipeline and gates](docs/simulation-pipeline.md) · [Technology choices and locations](docs/technology-map.md).

**Goal:** physically distinct 2D chickens, independently trained models, reusable event histories and recordings.

Checked = source/scaffold present, untested here. IDs and dependencies remain stable; pass each gate before dependent work.

## Existing work

- [x] E01: Simulation type enum.
- [x] E02: Training summary: Requested, Training, Trained, Failed, Cancelled.
- [x] E03: Simulation, Chicken and SimulationChicken entities.
- [x] E04: Composite participant key and relationships.
- [x] E05: EF DbContext and initial migration.
- [x] E06: Name/Contestants migration; Find/Race configuration/result scaffolds.
- [x] E07: Find POST saves simulation and configuration together.
- [x] E08: API controllers/services/DTOs and video-storage interface.
- [x] E09: Next layout/navigation and request scaffolding.
- [x] E10: Unity Find agent, goal/wall components, prefab and scene.
- [x] E11: Rigidbody2D, six observations, two actions, bounded episodes.
- [x] E12: Parallel environments currently sharing one policy.
- [x] E13: Committed Unity manifest, lockfile and Editor version.

## Stage 0 — Scope and reproducibility

**Depends:** repository. **Gate:** reproducible setup, body/task contract and status-data migration plan.

- [ ] S0.01: Confirm side-view/gravity; equivalent isolated lanes, shared clock, no chicken collisions.
- [ ] S0.02: Bound two appendage layouts, dimensions, mass and strength; explain unsupported bodies.
- [ ] S0.03: Set acceptance metrics, training/evaluation budgets and release scope.
- [ ] S0.04: Inventory CPU/GPU/memory, encoder, OS/drivers, disk and uplink.
- [ ] S0.05: Reconcile Unity/URP manifest-lockfile versions; retain a compatibility-tested Editor.
- [ ] S0.06: Make Python requirements installable; move commands to setup documentation.
- [ ] S0.07: Verify documented ML-Agents/Python/PyTorch combination locally; lock resolved dependencies.
- [ ] S0.08: Document clean restores; resolve conflicts without blanket unpinned upgrades.
- [ ] S0.09: Audit reused status integers; back up, map provenance, flag ambiguity.
- [ ] S0.10: Define chicken, body, job, attempt, model, evaluation, event and recording.
- [ ] S0.11: Distinguish roster size, parallel arenas and concurrent jobs.
- [ ] S0.12: Define immutable artifact paths, storage budget and source-control exclusions.

## Stage 1 — Find learning baseline

**Depends:** 0. **Gate:** manually trained standalone Find meets held-out criteria.

- [ ] S1.01: Specify observations, actions and rewards; cover goals, walls, timeouts, bounds and invalid states.
- [ ] S1.02: Choose consistent Rigidbody2D movement; remove conflicting Transform control.
- [ ] S1.03: Match manual/learned bounds, diagonal speed, timestep and decision interval.
- [ ] S1.04: Fully reset pose, velocities, contacts, timers, reward and terminal state.
- [ ] S1.05: Seed valid starts/goals; separate smoke, evaluation and final benchmarks.
- [ ] S1.06: Normalize observations; record one authoritative terminal reason per episode.
- [ ] S1.07: Version configuration/results; apply effective settings before agent initialization.
- [ ] S1.08: Replace action logs with aggregate metrics and bounded diagnostics.
- [ ] S1.09: Establish manual/random/scripted baselines; check reset and collisions.
- [ ] S1.10: Version small vector PPO recipe matching the behavior name.
- [ ] S1.11: Train Editor then no-graphics standalone; record configuration and versions.
- [ ] S1.12: Save checkpoints/ONNX/TensorBoard; prove graceful interruption, continuation and export.
- [ ] S1.13: Benchmark CPU/GPU and arenas; distinguish wall/simulated time.
- [ ] S1.14: Evaluate held-out scenarios across training seeds; retain outcomes and baseline comparisons.
- [ ] S1.15: Test controls/observations/reset/termination; document reproducible smoke runs.

## Stage 2 — Independent physical chickens

**Depends:** 1. **Gate:** two different bodies learn useful movement independently.

- [ ] S2.01: Serialize versioned/hashed topology, appendages, dimensions, mass, colliders, anchors and motors.
- [ ] S2.02: Assemble stable Rigidbody2D/Collider2D bodies with bounded joints/motors.
- [ ] S2.03: Validate contacts, self-collision, mass, joints, clearance and complete body reset.
- [ ] S2.04: Prove manual motor success; distinguish impossible bodies from training faults.
- [ ] S2.05: Version ordered joint/contact/velocity/goal observations and bounded motor actions.
- [ ] S2.06: Drive physical actuators; prevent hidden Transform-assisted locomotion.
- [ ] S2.07: Define falls, stalls and timeouts; measure stability, progress, success and effort separately from reward.
- [ ] S2.08: Train independent initialization; preserve seed/body/checkpoint/log ownership.
- [ ] S2.09: Validate balance/movement/goal curriculum if simpler learning steps are needed.
- [ ] S2.10: Add second appendage layout/size variation; validate physics before training.
- [ ] S2.11: Train second chicken separately with independent weights and artifacts.
- [ ] S2.12: Parallelize identical chicken/body copies only; prevent cross-chicken policy sharing.
- [ ] S2.13: Evaluate held-out conditions/repeated seeds; record compute and learnability limits.
- [ ] S2.14: Invalidate models after physical revisions; preserve compatibility after cosmetic changes.
- [ ] S2.15: Test assembly/schema rejection; defer unlimited morphology editing.

## Stage 3 — Evaluation and model delivery

**Depends:** 2. **Gate:** unchanged player runs both independent models/rigs simultaneously, including a post-build model.

- [ ] S3.01: Freeze evaluation protocol/inference; verify weights never change.
- [ ] S3.02: Define candidate/approved/rejected decisions, thresholds, benchmark versions and evaluation reports.
- [ ] S3.03: Import ONNX ModelAsset; bind correct body in rendered standalone.
- [ ] S3.04: Prove post-build ModelAsset delivery through isolated, platform-specific AssetBundle packaging.
- [ ] S3.05: Validate dependencies, tensors, metadata, binding, disposal and repeated loading.
- [ ] S3.06: Document Model/ModelAsset boundary; evaluate native packaging before alternative adapters.
- [ ] S3.07: Budget Editor licensing/runtime; reserve isolated workspace, never active developer project.
- [ ] S3.08: Manifest separate checkpoints, ONNX, packages, configuration and evaluation.
- [ ] S3.09: Verify hashes and chicken/body/task/rules/policy/build compatibility; reject corruption.
- [ ] S3.10: Compare rendered/headless evaluation; select CPU-compatible no-graphics inference.
- [ ] S3.11: Repeat simultaneous heterogeneous model/rig runs; prevent accidental binding reuse.
- [ ] S3.12: Document build/import/run procedures and limitations before automation.

## Stage 4 — Durable training and API/UI

**Depends:** 3. Freeze contracts before parallel API/UI/worker work.
**Gate:** individual roster training survives restart/failure with accurate progress/history.

- [ ] S4.01: Migrate reviewed statuses; align DBML/enums/DTOs/web and nullable UTC timestamps.
- [ ] S4.02: Persist owners, bodies, jobs, attempts, models and evaluations with constraints/indexes.
- [ ] S4.03: Transactionally snapshot roster/chicken/body/task plus jobs/outbox; tolerate duplicate Hangfire dispatch.
- [ ] S4.04: Reuse approved compatible models; retry failures without retraining successful chickens.
- [ ] S4.05: Validate names/counts/finite bounds/bodies/budgets and server-selected recipes/builds.
- [ ] S4.06: Replace stubs with get/list/status/history/cancel and bounded retry.
- [ ] S4.07: Standardize Problem Details/pagination/idempotency; fix forbidden/not-found/conflict and Forbid(description) scheme misuse.
- [ ] S4.08: Generate OpenAPI types, use openapi-fetch, detect drift; remove or map CreatedAt.
- [ ] S4.09: Prove pinned Hangfire/SqlServer Worker; scoped DbContext, one queue, WorkerCount=1, single host.
- [ ] S4.10: Use Hangfire scheduling; retain attempt ownership/heartbeats, stale-write fencing and startup recovery.
- [ ] S4.11: Define aggregate readiness/partial failure; distinguish rejection from process failure.
- [ ] S4.12: Supervise trainer/Unity/evaluator/packager using server-owned arguments and private attempt directories.
- [ ] S4.13: Drain stdout/stderr; capture exits/progress; enforce startup/wall-clock/step budgets.
- [ ] S4.14: Persist cancellation; bound graceful saves; verify process-tree cleanup and completion races.
- [ ] S4.15: Start exception retries at zero; handle redelivery; preserve attempts; prove bounded transient retries.
- [ ] S4.16: Validate manifests/hashes; publish atomically before references; recover file/SQL crash gaps.
- [ ] S4.17: Add ILogger, ActivitySource/Meter, health/queue/duration/disk metrics; bound retention and propagate job traces.
- [ ] S4.18: Build React/SVG creator, Zod validation, roster/Find requests and individual progress.
- [ ] S4.19: Add SWR polling/backoff, loading/empty/errors, cancel/retry and separate evaluation metrics.
- [ ] S4.20: Test competing SQL claims, stale owners, cancellation, crashes, timeouts and missing/corrupt outputs.
- [ ] S4.21: Simulate process faults; verify real training/evaluation/packaging and approved-model readback.
- [ ] S4.22: Add xUnit/WebApplicationFactory/Testcontainers/Playwright plus web/Unity checks; isolate expensive smoke runs.
- [ ] S4.23: Prove the selected auth stack locally/on Vercel; test expiry, refresh, CSRF, origins and ownership; confirm provider plan.

## Stage 5 — Scored events and history

**Depends:** 4. **Gate:** one durable frozen-policy event; viewing creates no work.

- [ ] S5.01: Implement Find with equivalent isolated lanes, fair spawns, shared countdown/clock and goals.
- [ ] S5.02: Define ties, finish order, timeouts, falls, disqualification and non-finishers.
- [ ] S5.03: Snapshot SimulationRun/SimulationRunParticipant roster/body/model/lane/rules/seed; preserve history separately from default roster.
- [ ] S5.04: Replace no-op start with explicit start/cancel and idempotent jobs.
- [ ] S5.05: Run independent frozen models; retain one compute slot until benchmarked.
- [ ] S5.06: Validate machine-readable participant results, roster membership, values and complete termination.
- [ ] S5.07: Migrate Find/Race results to events; add necessary mode detail.
- [ ] S5.08: Commit results once; derive or idempotently cache win/history statistics.
- [ ] S5.09: Build event lists/details, model/body history and chicken statistics.
- [ ] S5.10: Test duplicates/winners/ties/non-finishers/models/crashes; preserve interrupted events; rematches require new IDs.
- [ ] S5.11: Verify subsequent competitions get new IDs without changing prior results.
- [ ] S5.12: Preserve seeds/configuration/builds; avoid promises of deterministic physics replay.

## Stage 6 — Recordings and local live viewing

**Depends:** 5; synthetic media can precede Unity capture.
**Gate:** replay matches results; viewers share one event process.

- [ ] S6.01: Set resolution/FPS/quality/latency targets; verify actual encoder capabilities.
- [ ] S6.02: Prove FFmpeg synthetic source → MediaMTX → browser and finalized recording.
- [ ] S6.03: Prove unattended rendering; retain separate no-graphics training/evaluation profiles.
- [ ] S6.04: Frame cameras, identify contestants, show timer and results.
- [ ] S6.05: Benchmark RenderTexture/AsyncGPUReadback/FFmpeg; verify orientation/format/pacing/backpressure.
- [ ] S6.06: Bound capture memory/queues; drop frames without altering physics/scoring.
- [ ] S6.07: Record complete finite event; correlate video and simulation clocks.
- [ ] S6.08: Persist independent media metadata/states: Pending/Live/Finalizing/Ready/Failed.
- [ ] S6.09: Implement storage/indexing/finalization/retention; recover disk-full and partial files.
- [ ] S6.10: Authorize playback/MP4 delivery; distinguish paths/URLs and live/VOD.
- [ ] S6.11: Build seekable replay with honest unavailable/finalizing states.
- [ ] S6.12: Add HLS/LL-HLS detection, hls.js/native playback, reconnect/stall/end handling and cleanup.
- [ ] S6.13: Reconcile duplicate/missed media hooks through artifact/state inspection.
- [ ] S6.14: Verify shared viewing and uninterrupted recording after viewer disconnects.
- [ ] S6.15: Test browsers/long events/media failures; preserve valid results independently.

## Stage 7 — Public release and operations

**Depends:** 6. **Gate:** protected external create/train/run/watch/history and proven recovery.

- [ ] S7.01: Complete identity proof; enforce ownership and public/private/unlisted media access.
- [ ] S7.02: Enforce user/global queue quotas, compute/rate limits and bounded inputs.
- [ ] S7.03: Configure HTTPS tunnel/origins/secrets; keep SQL/worker/management endpoints private.
- [ ] S7.04: Compare Stream versus MediaMTX/Caddy costs; budget trial before provisioning; require latency/live/replay proof.
- [ ] S7.05: Prove outbound SRT/reconnects; reconcile Stream IDs/readiness or self-hosted forwarding/TLS.
- [ ] S7.06: Separate publish/view credentials; hide long-lived secrets and internal paths.
- [ ] S7.07: Configure recording/CORS/seeking/retention; verify desktop/mobile live and replay.
- [ ] S7.08: Measure startup/latency/viewer capacity and home/public bandwidth.
- [ ] S7.09: Choose Windows Services/interactive Task Scheduler; test lock/disconnect/logout/reboot; queue rendering without available session.
- [ ] S7.10: Budget OpenTelemetry backend; consider Grafana Cloud; alert on queue/failure/heartbeat/storage/capture/media with bounded labels.
- [ ] S7.11: Back up SQL/artifacts; prove restore; document retention/failed-attempt cleanup.
- [ ] S7.12: Document versioned deployment/migration/rollback and offline-host/WAN/disk-full recovery.
- [ ] S7.13: Test quotas/authorization/reconnect/load without duplicate jobs or corrupted results.
- [ ] S7.14: Finish About/help covering individuality, learning, lifecycle, evaluation and limitations.

## Stage 8 — Modes and richer bodies

**Depends:** 5; parallel with 6–7. **Gate:** each mode passes physics/learning/scoring/playback checks.

- [ ] S8.01: Specify Race lanes, checkpoints, starts, obstacles, finishes, ties and non-finishers.
- [ ] S8.02: Train versioned Race policies; continue only owned, compatible, recorded checkpoints.
- [ ] S8.03: Optionally define scored Balance separately from learning curriculum.
- [ ] S8.04: Define Sumo contacts, bounds, falls, simultaneous elimination, stalls and duration.
- [ ] S8.05: Baseline scripted/frozen opponents before self-play; verify heterogeneous body/control support.
- [ ] S8.06: Version opponents and independent learner checkpoints; test held-out opponents/exploits.
- [ ] S8.07: Expand templates/appendages/sizes incrementally; validate mass/joint/collision stability.
- [ ] S8.08: Improve creator previews/feedback; require retraining after physical changes.
- [ ] S8.09: Benchmark morphology families; reject invalid bodies; report exhausted learning budgets.
- [ ] S8.10: Add rules/configuration/results/UI/evaluation recipes when each mode is enabled.
- [ ] S8.11: Preserve representative recorded regression events across supported modes/bodies.

## Stage 9 — Analytics, capacity and research

**Depends:** public-operation or expanded-mode evidence, as applicable; optional for first release.

- [ ] S9.01: Estimate frozen-event probabilities: NumPy/pandas/SciPy/Matplotlib, isolated environment, declared rules/seeds/sample counts.
- [ ] S9.02: Report uncertainty, frequency baselines and held-out calibration; budgets aren't odds.
- [ ] S9.03: Define rules/model changes/opponent sampling before tournaments and ratings.
- [ ] S9.04: Profile scheduling/physics/training/packaging/rendering/encoding before concurrency.
- [ ] S9.05: Scale workers for measured demand; preserve ownership and artifact accessibility.
- [ ] S9.06: Evaluate R2/S3 durability/retention/versioning/recovery/interrupted uploads/browser access separately from live delivery.
- [ ] S9.07: Replace Hangfire/SQL only for measured scheduling or operational limitations.
- [ ] S9.08: Consider WebRTC for unmet latency requirements; measure connectivity/operating costs.
- [ ] S9.09: Explore snapshot/action/state replay and drift only for unmet video-replay needs.
- [ ] S9.10: Explore morphology search/advanced RL after stable physics constraints and evaluation.

Start with Stage 0. Prove independent bodies in Stage 2 and model delivery in Stage 3 before automation. This revision changes documentation only.
