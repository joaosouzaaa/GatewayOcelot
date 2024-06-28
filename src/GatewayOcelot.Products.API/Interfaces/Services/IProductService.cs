using GatewayOcelot.Products.API.DataTransferObjects.Product;
using GatewayOcelot.Products.API.Settings.PaginationSettings;

namespace GatewayOcelot.Products.API.Interfaces.Services;

public interface IProductService
{
    Task AddAsync(ProductCreateRequest productCreateRequest);
    Task UpdateAsync(ProductUpdateRequest productUpdateRequest);
    Task DeleteAsync(Guid id);
    Task<ProductResponse?> GetByIdAsync(Guid id);
    Task<PageList<ProductResponse>> GetAllPaginatedAsync(PageParameters pageParameters);
}
