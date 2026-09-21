using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NeuralChickens.Api.Application.Interfaces;
using NeuralChickens.Api.Application.Services;
using NeuralChickens.Api.Domain;

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

builder.Services.AddDbContextPool<NeuralChickensDbContext>(options =>
{
    options.UseSqlServer(connectionBuilder.ConnectionString, sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("NeuralChickens.Api.Domain");
        sqlOptions.CommandTimeout(30);
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null
            );
    });
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
}, poolSize: 128);

var app = builder.Build();

app.MapControllers();

app.UseCors("Frontend");

app.UseHttpsRedirection();

app.Run();

 