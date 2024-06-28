using MongoDB.Driver;

namespace GatewayOcelot.Products.API.Interfaces.Infrastructure.DatabaseContexts;

public interface IMongoDbContext
{
    IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName);
}
