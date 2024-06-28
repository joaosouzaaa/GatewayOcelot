using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Settings.PaginationSettings;

namespace GatewayOcelot.Products.API.Interfaces.Infrastructure.Repositories;

public interface IProductRepository
{
    Task InsertOneAsync(Product product);
    Task ReplaceOneAsync(Product product);
    Task<bool> ExistsAsync(Guid id);
    Task DeleteOneAsync(Guid id);
    Task<Product?> GetByIdAsync(Guid id);
    Task<PageList<Product>> GetAllPaginatedAsync(PageParameters pageParameters);
}
