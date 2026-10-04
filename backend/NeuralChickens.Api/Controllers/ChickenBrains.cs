using Microsoft.AspNetCore.Mvc;
using NeuralChickens.Api.Application.DTOs.ChickenBrain;
using NeuralChickens.Api.Application.Interfaces;

namespace NeuralChickens.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChickenBrains(IChickenBrainService chickenBrainService) : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PostChickenBrain(PostChickenBrainDto postChickenBrainDto)
        {
            var result = await chickenBrainService.CreateChickenBrainAsync(postChickenBrainDto);

            return ToActionResult(result);
        }


    }
}
