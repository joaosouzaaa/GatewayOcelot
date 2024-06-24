using GatewayOcelot.API.DataTransferObjects.Product;
using GatewayOcelot.API.Interfaces.Services;
using GatewayOcelot.API.Settings.NotificationSettings;
using GatewayOcelot.API.Settings.PaginationSettings;
using Microsoft.AspNetCore.Mvc;

namespace GatewayOcelot.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task AddAsync([FromBody] ProductCreateRequest productCreateRequest) =>
        productService.AddAsync(productCreateRequest);

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task UpdateAsync([FromBody] ProductUpdateRequest productUpdateRequest) =>
        productService.UpdateAsync(productUpdateRequest);

    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task DeleteAsync([FromQuery] Guid id) =>
        productService.DeleteAsync(id);

    [HttpGet("get-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<ProductResponse?> GetByIdAsync([FromQuery] Guid id) =>
        productService.GetByIdAsync(id);

    [HttpGet("get-all-paginated")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageList<ProductResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<PageList<ProductResponse>> GetAllPaginatedAsync([FromQuery] PageParameters pageParameters) =>
        productService.GetAllPaginatedAsync(pageParameters);
}
