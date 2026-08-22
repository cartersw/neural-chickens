using NeuralChickens.Api.Common.Results;

namespace NeuralChickens.Api.Application.Interfaces
{
    public interface ISimulationService
    {
        Task<Result> GetSimulationResultAsync(int id);
    }
}