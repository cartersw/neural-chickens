using Microsoft.AspNetCore.Mvc;

namespace NeuralChickens.Api.Controllers
{
    public class Simulations : ApiControllerBase
    {
        public async Task<IActionResult> FirstEndpoint()
        {
            return BadRequest();
        }
    }
}
