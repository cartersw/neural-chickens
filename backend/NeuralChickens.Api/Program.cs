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

var app = builder.Build();

app.MapControllers();

app.UseCors("Frontend");

app.UseHttpsRedirection();

app.Run();

 