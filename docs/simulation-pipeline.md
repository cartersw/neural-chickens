# Simulation pipeline
 ```mermaid
sequenceDiagram
    participant UI
    participant API
    participant DB
    participant Worker
    participant Run as Training run (1 per chicken)
    participant Storage

    UI->>API: Create chicken (name, properties)
    API->>DB: Insert Chicken + pending ChickenBrain (Requested)
    API-->>UI: Created (status Requested)

    Worker->>DB: Claim next Requested job (Training, lease)
    Worker->>Run: Start mlagents-learn (per-job YAML, own port range)
    Run->>Run: Launch Unity instances and train
    Run-->>Worker: Exit code + .onnx
    Worker->>Storage: Save .onnx
    Worker->>DB: Set BrainPath, status Trained (or Failed)

    UI->>API: Get chicken status
    API->>DB: Read status
    API-->>UI: Trained
```