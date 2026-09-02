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