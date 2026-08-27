# Tasks

## Simulation Api
- [x] Simulation Type Enum
- [x] Simulation Status Enum
- [x] Simulation Entity Class
- [x] Chicken Entity Class
- [x] Simulation Chicken Entity Class
- [ ] Composite PK configuration Simulation Chicken 
- [ ] Create dbcontext
- [ ] First db migration
- [ ] Decide how to handle different result types

## Unity Worker
- [ ] External simulation configuration
- [ ] Headless Unity executable
- [ ] Simulation job runner
- [ ] Asynchronous simulation queue
- [ ] Asynchronous ML-Agent training
- [ ] Training/result persistence
- [ ] Agent performance evaluation
- [ ] Rigidbody agent framework
- [ ] Customizable limbs
- [ ] Race environment
- [ ] Sumo environment
- [ ] Replay / livestream system

POST /api/simulations
{
    "targetX": 8,
    "moveSpeed": 3
}
       ↓
Simulation 1 inserted
Status = Queued
       ↓
Worker notices Simulation 1
       ↓
Worker starts Unity headless
       ↓
Unity receives:
targetX = 8
moveSpeed = 3
       ↓
Your existing MoveToGoalAgent trains
       ↓
training stops
       ↓
ONNX saved
       ↓
Simulation 1
Status = Completed
ModelPath = ...
Reward = ...


## Frontend
- [ ] 


## Misc