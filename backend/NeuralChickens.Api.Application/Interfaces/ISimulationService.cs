using NeuralChickens.Api.Application.DTOs.Simulation;
using NeuralChickens.Api.Common.Results;

namespace NeuralChickens.Api.Application.Interfaces
{
    public interface ISimulationService
    {
        Task<Result<GetSimulationDto>> CreateFindSimulationAsync(PostFindSimulationDto postFindSimulationDto);
        Task<Result<GetSimulationDto>> GetSimulationAsync(int id);
        Task<Result> GetSimulationVideoAsync(int id);
        Task<Result> StartSimulationAsync(int id);
    }
}