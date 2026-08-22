using NeuralChickens.Api.Application.Contracts;
using NeuralChickens.Api.Application.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

builder.Services.AddScoped<ISimulationService, SimulationService>();

var app = builder.Build();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

 