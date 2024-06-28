using GatewayOcelot.Products.API.Constants;
using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.DatabaseContexts;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.Repositories;
using GatewayOcelot.Products.API.Settings.PaginationSettings;
using MongoDB.Driver;

namespace GatewayOcelot.Products.API.Infrastructure.Repositories;

public sealed class ProductRepository(IMongoDbContext dbContext) : IProductRepository
{
    private readonly IMongoCollection<Product> _collection = dbContext.GetCollection<Product>(CollectionsConstants.ProductCollection);

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
