using Microsoft.EntityFrameworkCore;
using NeuralChickens.Api.Common.Enums;
using NeuralChickens.Api.Domain;


namespace NeuralChickens.TrainingWorker
{
    public class Worker(
        IServiceScopeFactory scopeFactory,
        ILogger<Worker> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await using (var scope = scopeFactory.CreateAsyncScope())
                {
                    var context = scope.ServiceProvider
                        .GetRequiredService<NeuralChickensDbContext>();

                    var pendingCount = await context.Simulations.CountAsync(
                        simulation =>
                        simulation.SimulationType == SimulationType.Find &&
                        simulation.SimulationStatus == SimulationStatus.Requested,
                        stoppingToken);

                    logger.LogInformation(
                        "There are {Count} waiting",
                        pendingCount);
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(5),
                    stoppingToken);
            }
        }
    }
}
