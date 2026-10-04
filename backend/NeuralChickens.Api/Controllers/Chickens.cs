using Microsoft.AspNetCore.Mvc;
using NeuralChickens.Api.Application.Interfaces;

namespace NeuralChickens.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Chickens(IChickenService chickenService) : ApiControllerBase
    {

    }
}
