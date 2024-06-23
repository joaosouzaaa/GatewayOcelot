using Microsoft.AspNetCore.Mvc;

namespace GatewayOcelot.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProductController : ControllerBase
{
    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public void GetAll()
    {

    }
}
