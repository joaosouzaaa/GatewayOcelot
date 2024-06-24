using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Settings.PaginationSettings;

namespace GatewayOcelot.API.Interfaces.Data.Repositories;

public interface IProductRepository
{
    Task InsertOneAsync(Product product);
    Task ReplaceOneAsync(Product product);
    Task<bool> ExistsAsync(Guid id);
    Task DeleteOneAsync(Guid id);
    Task<Product?> GetByIdAsync(Guid id);
    Task<PageList<Product>> GetAllPaginatedAsync(PageParameters pageParameters);
}
