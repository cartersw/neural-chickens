using NeuralChickens.Api.Application.DTOs.Simulation;
using NeuralChickens.Api.Application.Interfaces;
using NeuralChickens.Api.Common.Enums;
using NeuralChickens.Api.Common.Results;
using NeuralChickens.Api.Domain;
using NeuralChickens.Api.Domain.Entities;

namespace NeuralChickens.Api.Application.Services
{
    public class SimulationService(NeuralChickensDbContext context) : ISimulationService
    {

        public async Task<Result<GetSimulationDto>> GetSimulationAsync(int id)
        {
            var getSimulationDto = new GetSimulationDto
            {
                Id = 5,
                SimulationType = "Race",
                Status = "Completed",
                RequestedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                StartedAt = DateTime.UtcNow,
                CompletedAt = DateTime.UtcNow
            };

            return Result<GetSimulationDto>.Success(getSimulationDto);
        }
        public async Task<Result> StartSimulationAsync(int id)
        {
            return Result.Success();
        }

        public async Task<Result> GetSimulationVideoAsync(int id)
        {
            return Result.Success();
        }

        public async Task<Result<GetSimulationDto>> CreateFindSimulationAsync(PostFindSimulationDto postFindSimulationDto)
        {
            var simulation = new Simulation
            {
                Name = postFindSimulationDto.Name,
                SimulationType = SimulationType.Find,
                SimulationStatus = SimulationStatus.Requested,
                Contestants = postFindSimulationDto.Contestants,
                RequestedAt = DateTime.UtcNow,
            };


            var findSimulationConfiguration = new FindSimulationConfiguration
            {
                Simulation = simulation,
                Speed = postFindSimulationDto.Speed
            };

            context.Simulations.Add(simulation);
            context.FindSimulationConfigurations.Add(findSimulationConfiguration);
            await context.SaveChangesAsync();

            var getSimulationDto = new GetSimulationDto
            {
                Name = simulation.Name,
                Id = simulation.Id,
                SimulationType = simulation.SimulationType.ToString(), 
                Status = simulation.SimulationStatus.ToString(),         
                Contestants = simulation.Contestants,
                RequestedAt = simulation.RequestedAt,
                StartedAt = simulation.StartedAt,
                CompletedAt = simulation.CompletedAt,
            };

            return Result<GetSimulationDto>.Success(getSimulationDto);

        }


    }
}