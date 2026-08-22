using NeuralChickens.Api.Common.Results;

namespace NeuralChickens.Api.Application.Interfaces
{
    public interface ISimulationService
    {
        Task<Result> GetSimulationAsync(int id);
        Task<Result> GetSimulationVideoAsync(int id);
        Task<Result> StartSimulationAsync(int id);
    }
}