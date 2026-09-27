
## Entities and schema

- [ ] Add the chicken's properties (starting with `Speed`) to `Chicken` and remove `Speed` from `FindSimulationConfiguration`.
- [ ] Add `TrainingStatus`, `ClaimId`, `LeaseExpiresAt`, and `CompletedAt` to `ChickenBrain`, and make `BrainPath` nullable until training finishes.
- [ ] Decide how edits to chicken properties work: either make them unchangeable, or copy them onto each `ChickenBrain` when training starts.
- [ ] Add `DbSet<ChickenBrain> ChickenBrains` to `NeuralChickensDbContext` so the table gets a plural name like the others.
- [ ] Change `SimulationChickens` to reference `ChickenBrainId` so each simulation records exactly which brain ran.
- [ ] Add each simulation type's own settings (track size, time limit, and so on) to its configuration table.
- [ ] Give `Simulation` its own `ClaimId` and `LeaseExpiresAt` columns for the job that runs it.
- [ ] Update `ChickenBrainConfiguration` and `SimulationChickenConfiguration` for the new columns, the nullable `BrainPath`, and the new brain foreign key.
- [ ] Add a migration and update `Schema.dbml` to match.

## Enums

- [ ] Rename the current `SimulationStatus` to `TrainingStatus` and use it on `ChickenBrain`.
- [ ] Create a new `SimulationStatus` for simulation runs, such as `Requested`, `Running`, `Completed`, and `Failed`.

## API and services

- [ ] Add a `Chickens` controller and service with an endpoint that creates a chicken and a requested `ChickenBrain` for the chosen `SimulationType`.
- [ ] Add an endpoint that requests a new brain for an existing chicken.
- [ ] Add an endpoint that returns a chicken with its brains and their training status.
- [ ] Replace `Speed` in `PostFindSimulationDto` with Find-specific settings and an optional list of chicken IDs.
- [ ] Make `CreateFindSimulationAsync` check that each provided chicken has a trained Find brain, pick its latest brain, and fill the empty slots with random trained chickens.
- [ ] Decide whether a simulation request with too few trained chickens is rejected or runs with fewer contestants.
- [ ] Decide whether the `StartSimulation` endpoint is still needed, since a background runner will pick up requested simulations automatically.
- [ ] Add the contestant list (chickens and their brains) to `GetSimulationDto`.

## Training jobs

- [ ] Change `TrainingJobClaimDto` to carry `ChickenBrainId`, the `SimulationType`, and the chicken properties the worker needs.
- [ ] Implement `TrainingJobStore.TryClaimNextAsync` against `ChickenBrains` instead of `Simulations`.
- [ ] Add store methods to renew a lease, mark a brain `Trained` with its `BrainPath`, and mark it `Failed`.
- [ ] Add a brain storage interface, like `IVideoStorage`, for saving `.onnx` files.

## Worker

- [ ] Implement the `Worker` loop so it claims a brain, writes a per-job YAML file, starts `mlagents-learn` with its own run ID and port range, and records the result.
- [ ] Add a concurrency limit so several chickens can train at the same time.
- [ ] Add a separate simulation runner, either as a second loop or a second worker, that claims requested simulations, runs Unity with the chosen brains, and saves the video.

## Unity

- [ ] Make the agent load a specific `.onnx` model and apply that chicken's properties when running a simulation.
- [ ] Scale observations by arena size, or randomize environment settings during training, so brains still work when simulation settings change.
- [ ] Create a headless build for training and a separate build that records video for simulations.

## Web

- [ ] Add a create-chicken form with the chicken's properties and a simulation-type picker.
- [ ] Update the chickens page to show a chicken's brains and their training status.
- [ ] Update the simulation form so users pick chickens instead of setting a speed.
- [ ] Update the `SimulationInfo` type to match the new `GetSimulationDto`.

