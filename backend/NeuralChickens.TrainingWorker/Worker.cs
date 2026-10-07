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

                    

                    logger.LogInformation(
                        "There are {Count} waiting",
                        context.BrainProcessingJobs.CountAsync());
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(5),
                    stoppingToken);
            }
        }
    }
}
