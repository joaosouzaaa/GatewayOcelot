using GatewayOcelot.Recommendations.API.Entities;
using GatewayOcelot.Recommendations.API.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GatewayOcelot.Recommendations.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class RecommendationController(IRecommendationRepository recommendationRepository) : ControllerBase
{
    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Recommendation>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<Recommendation>> GetAllAsync() =>
        recommendationRepository.GetAllAsync();
}
