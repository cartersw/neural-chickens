using Microsoft.AspNetCore.Mvc;
using NeuralChickens.Api.Application.Interfaces;


namespace NeuralChickens.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Simulations(ISimulationService simulationService) : ApiControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSimulation(int id)
        {
            var result = await simulationService.GetSimulationAsync(id);

            return ToActionResult(result);
        }

        [HttpPost("{id}/start")]
        public async Task<IActionResult> StartSimulation(int id)
        {
            var result = await simulationService.StartSimulationAsync(id);

            return ToActionResult(result);
        }

        [HttpGet("{id}/video")]
        public async Task<IActionResult> GetSimulationVideo(int id)
        {
            var result = await simulationService.GetSimulationVideoAsync(id);

            return ToActionResult(result);
        }
        
    }
}
