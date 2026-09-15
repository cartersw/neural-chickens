using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NeuralChickens.Api.BackgroundServices
{
    public class SimulationTrainingWorker(
        ILogger<SimulationTrainingWorker> logger
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            logger.LogInformation("Training worker started.");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.LogInformation("The background service is running");
                    await Task.Delay(
                        TimeSpan.FromSeconds(3),
                        stoppingToken
                    );
                }
            }

            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {

            }


            logger.LogInformation("Training worker stopped");
        }
    }
}
