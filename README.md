# Neural Chickens
https://neural-chickens.vercel.app/

## Setup


### Backend
```bash
dotnet tool install --global dotnet-ef
```

In NeuralChickens.Api:
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:NeuralChickensLocalDb" "Server=localhost,1400;    Database=NeuralChickensLocalDb;    User Id=sa;    Password=LocalPassword1!;    TrustServerCertificate=True;"
```

Migrations:
```bash
dotnet ef migrations add InitialCreate --project NeuralChickens.Api.Domain --startup-project NeuralChickens.Api
dotnet ef database update --project NeuralChickens.Api.Domain --startup-project NeuralChickens.Api
```