using Microsoft.Data.SqlClient;
using NeuralChickens.Api.Application.Interfaces;
using NeuralChickens.Api.Application.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddScoped<ISimulationService, SimulationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var connectionString = builder.Configuration.GetConnectionString("NeuralChickensLocalDb");

var connectionBuilder = new SqlConnectionStringBuilder(connectionString);

if (builder.Environment.IsEnvironment("Testing"))
{
    connectionBuilder.InitialCatalog = "NeuralChickensTestDb";
}

var app = builder.Build();

app.MapControllers();

app.UseCors("Frontend");

app.UseHttpsRedirection();

app.Run();

 