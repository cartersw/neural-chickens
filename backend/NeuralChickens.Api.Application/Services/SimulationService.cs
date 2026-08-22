using NeuralChickens.Api.Application.DTOs.Simulation;
using NeuralChickens.Api.Application.Interfaces;
using NeuralChickens.Api.Common.Results;

namespace NeuralChickens.Api.Application.Services
{
    public class SimulationService : ISimulationService
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

     
    }
}