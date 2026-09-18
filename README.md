# Neural Chickens
https://neural-chickens.vercel.app/

## Setup
NeuralChickensSimulator
```bash
venv\Scripts\activate
mlagents-learn --run-id=test1
```
Press play on Unity Project

### Backend

```bash
dotnet tool install --global dotnet-ef
```

In NeuralChickens.Api:
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:NeuralChickensLocalDb" "Server=localhost,1400;    Database=NeuralChickensLocalDb;    User Id=sa;    Password=LocalPassword1!;    TrustServerCertificate=True;"
```

Create Docker Container:
```bash
docker run -d `
  --name neuralchickens-sql `
  -e "ACCEPT_EULA=Y" `
  -e "MSSQL_SA_PASSWORD=LocalPassword1!" `
  -p 1400:1433 `
  -v neuralchickens-sqldata:/var/opt/mssql `
  mcr.microsoft.com/mssql/server:2022-latest
```

Migrations:
```bash
dotnet ef migrations add InitialCreate --project NeuralChickens.Api.Domain --startup-project NeuralChickens.Api
dotnet ef database update --project NeuralChickens.Api.Domain --startup-project NeuralChickens.Api
```

Start Training Queue:
```bash
dotnet run --project backend/NeuralChickens.Api --launch-profile http
```
Test Find Simulation:
```bash
$simulation = Invoke-RestMethod -Method Post `
  -Uri http://localhost:5296/api/simulations/find `
  -ContentType application/json `
  -Body '{"name":"Find test","contestants":1,"speed":4.5}'

$simulation
```


### Simulator

#using python 3.10
#winget install Python.Python.3.10

Unity Setup:
In unity add and open: 
simulator/NeuralChickensSimulator

Build the player at:
simulator/NeuralChickensSimulator/Builds/Find/NeuralChickens.exe

Venv Setup:
```bash
py -3.10 -m venv simulator/NeuralChickensSimulator/venv

$trainingPython = "./simulator/NeuralChickensSimulator/venv/Scripts/python.exe"

& $trainingPython -m pip install --upgrade pip "setuptools<81"
& $trainingPython -m pip install "mlagents==1.1.0" "torch==2.1.1"

& $trainingPython -m mlagents.trainers.learn --help
```