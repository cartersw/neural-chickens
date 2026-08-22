using NeuralChickens.Api.Application.Interfaces;
using NeuralChickens.Api.Common.Results;

namespace NeuralChickens.Api.Application.Services
{
    public class SimulationService : ISimulationService
    {

        public Task<Result> GetSimulationAsync(int id)
        {
            throw new NotImplementedException();
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