using NeuralChickens.Api.Application.Contracts;
using NeuralChickens.Api.Common.Results;

namespace NeuralChickens.Api.Application.Services
{
    public class SimulationService : ISimulationService
    {
        public async Task<Result> GetSimulationResultAsync(int id)
        {
            return Result.Success();
        }
    }
}