using GatewayOcelot.API.Constants;
using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Interfaces.Data.DatabaseContexts;
using GatewayOcelot.API.Interfaces.Data.Repositories;
using GatewayOcelot.API.Settings.PaginationSettings;
using MongoDB.Driver;

namespace GatewayOcelot.API.Data.Repositories;

public sealed class ProductRepository(IMongoDbContext dbContext) : IProductRepository
{
    private IMongoCollection<Product> _collection = dbContext.GetCollection<Product>(CollectionsConstants.ProductCollection);

    public Task InsertOneAsync(Product product) =>
        _collection.InsertOneAsync(product);

    public Task ReplaceOneAsync(Product product) =>
        _collection.ReplaceOneAsync(p => p.Id == product.Id, product);

    public Task<bool> ExistsAsync(Guid id) =>
        _collection.Find(p => p.Id == id).AnyAsync();

    public Task DeleteOneAsync(Guid id) =>
        _collection.DeleteOneAsync(p => p.Id == id);

    public Task<Product?> GetByIdAsync(Guid id) =>
        _collection.Find(p => p.Id == id).FirstOrDefaultAsync()!;

    public Task<PageList<Product>> GetAllPaginatedAsync(PageParameters pageParameters) =>
        _collection.Find(FilterDefinition<Product>.Empty).PaginateAsync(pageParameters);
}
