using NeuralChickens.Api.Common.Results;

namespace NeuralChickens.Api.Application.Contracts
{
    public interface ISimulationService
    {
        Task<Result> GetSimulationResultAsync(int id);
    }
}