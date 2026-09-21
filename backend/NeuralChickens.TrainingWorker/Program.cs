using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NeuralChickens.Api.Domain;
using NeuralChickens.TrainingWorker;

var builder = Host.CreateApplicationBuilder(args);


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


builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
