using Microsoft.AspNetCore.Mvc;

namespace NeuralChickens.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Simulations : ApiControllerBase
    {
        [HttpGet("{id}/result")]
        public async Task<IActionResult> GetSimulationResult()
        {
            return BadRequest();   
        }

        [HttpGet("{id}/video")]
        public async Task<IActionResult> GetSimulationVideo()
        {
            throw new NotImplementedException();
        }
    }
}
