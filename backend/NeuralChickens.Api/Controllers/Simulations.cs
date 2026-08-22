using Microsoft.AspNetCore.Mvc;
using NeuralChickens.Api.Application.Contracts;

namespace NeuralChickens.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Simulations(ISimulationService simulationService) : ApiControllerBase
    {
        [HttpGet("{id}/result")]
        public async Task<IActionResult> GetSimulationResult(int id)
        {
            var result = await simulationService.GetSimulationResultAsync(id);

            return ToActionResult(result);
        }

        [HttpGet("{id}/video")]
        public async Task<IActionResult> GetSimulationVideo()
        {
            throw new NotImplementedException();
        }
    }
}
