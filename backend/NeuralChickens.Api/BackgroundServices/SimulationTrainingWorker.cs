using System.Diagnostics;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using NeuralChickens.Api.Common.Enums;
using NeuralChickens.Api.Domain;
using YamlDotNet.RepresentationModel;

namespace NeuralChickens.Api.BackgroundServices;

public class SimulationTrainingWorker(
    IServiceScopeFactory scopeFactory,
    IConfiguration configuration,
    IHostEnvironment environment,
    ILogger<SimulationTrainingWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!configuration.GetValue<bool>("SimulationTraining:Enabled"))
            return;

        foreach (var key in new[] { "PythonExecutable", "ConfigPath", "EnvironmentPath" })
        {
            if (!File.Exists(ConfigPath(key)))
            {
                logger.LogError("SimulationTraining:{Key} file does not exist. Requested simulations remain queued.", key);
                return;
            }
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (await ProcessNextAsync(stoppingToken))
                    continue;
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Could not process the simulation queue.");
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    private async Task<bool> ProcessNextAsync(CancellationToken stoppingToken)
    {
        // Requested database rows are the queue. Run one enabled API instance.
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NeuralChickensDbContext>();
        var simulation = await db.Simulations
            .Where(s => s.SimulationType == SimulationType.Find && s.SimulationStatus == SimulationStatus.Requested)
            .OrderBy(s => s.RequestedAt).ThenBy(s => s.Id)
            .FirstOrDefaultAsync(stoppingToken);
        if (simulation is null)
            return false;

        simulation.SimulationStatus = SimulationStatus.Training;
        simulation.StartedAt = DateTime.UtcNow;
        simulation.CompletedAt = null;
        await db.SaveChangesAsync(stoppingToken);

        try
        {
            var settings = await db.FindSimulationConfigurations
                .SingleAsync(c => c.SimulationId == simulation.Id, stoppingToken);
            if (!float.IsFinite(settings.Speed) || settings.Speed <= 0)
                throw new InvalidOperationException("Find speed must be finite and greater than zero.");

            await TrainAsync(simulation.Id, settings.Speed, stoppingToken);
            simulation.SimulationStatus = SimulationStatus.Trained;
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            simulation.SimulationStatus = SimulationStatus.Cancelled;
        }
        catch (Exception exception)
        {
            simulation.SimulationStatus = SimulationStatus.Failed;
            logger.LogError(exception, "Training failed for simulation {SimulationId}.", simulation.Id);
        }

        simulation.CompletedAt = DateTime.UtcNow;
        using var saveTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await db.SaveChangesAsync(saveTimeout.Token);
        logger.LogInformation("Simulation {SimulationId}: {Status}.", simulation.Id, simulation.SimulationStatus);
        return true;
    }

    private async Task TrainAsync(int simulationId, float speed, CancellationToken stoppingToken)
    {
        var runId = $"simulation-{simulationId}-{Guid.NewGuid():N}";
        var runDirectory = Path.Combine(environment.ContentRootPath, "TrainingRuns", runId);
        Directory.CreateDirectory(runDirectory);

        var yaml = new YamlStream();
        using (var reader = File.OpenText(ConfigPath("ConfigPath")))
            yaml.Load(reader);
        var root = (YamlMappingNode)yaml.Documents[0].RootNode;
        if (!root.Children.TryGetValue("environment_parameters", out var parameters))
            root.Children["environment_parameters"] = parameters = new YamlMappingNode();
        ((YamlMappingNode)parameters).Children["speed"] = new YamlScalarNode(speed.ToString("R", CultureInfo.InvariantCulture));
        var runConfig = Path.Combine(runDirectory, "find.yaml");
        using (var writer = File.CreateText(runConfig))
            yaml.Save(writer, assignAnchors: false);

        var startInfo = new ProcessStartInfo(ConfigPath("PythonExecutable"))
        {
            WorkingDirectory = runDirectory,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        startInfo.Environment["PYTHONUNBUFFERED"] = "1";
        foreach (var argument in new[] { "-m", "mlagents.trainers.learn", runConfig, "--run-id", runId,
                     "--results-dir", Path.Combine(runDirectory, "results"),
                     "--env", ConfigPath("EnvironmentPath"), "--no-graphics" })
            startInfo.ArgumentList.Add(argument);

        using var process = new Process { StartInfo = startInfo };
        process.OutputDataReceived += (_, e) => { if (e.Data is not null) logger.LogInformation("ML-Agents: {Line}", e.Data); };
        process.ErrorDataReceived += (_, e) => { if (e.Data is not null) logger.LogInformation("ML-Agents: {Line}", e.Data); };
        logger.LogInformation("Starting simulation {SimulationId}, speed {Speed}. Run files: {RunDirectory}", simulationId, speed, runDirectory);
        stoppingToken.ThrowIfCancellationRequested();
        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        try
        {
            await process.WaitForExitAsync(stoppingToken);
        }
        finally
        {
            if (!process.HasExited)
            {
                process.Kill(entireProcessTree: true);
                await process.WaitForExitAsync();
            }
        }

        if (process.ExitCode != 0)
            throw new InvalidOperationException($"ML-Agents exited with code {process.ExitCode}.");
        if (!Directory.EnumerateFiles(runDirectory, "*.onnx", SearchOption.AllDirectories).Any())
            throw new InvalidOperationException("ML-Agents finished without producing a trained model.");
    }

    private string ConfigPath(string key) => Path.GetFullPath(
        configuration[$"SimulationTraining:{key}"] ?? throw new InvalidOperationException($"Missing SimulationTraining:{key}."),
        environment.ContentRootPath);
}
