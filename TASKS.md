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
- [ ] Receive simulation configuration from backend
- [ ] Headless Unity executable
- [ ] Simulation job runner
- [ ] Simulation queue manager
- [ ] Asynchronous ML-Agent training
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
- [ ] About
- [ ] Chickens page (chicken history and statistics view)


## Misc