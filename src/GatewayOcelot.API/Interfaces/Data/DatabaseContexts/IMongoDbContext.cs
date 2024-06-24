using MongoDB.Driver;

namespace GatewayOcelot.API.Interfaces.Data.DatabaseContexts;

public interface IMongoDbContext
{
    IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName);
}
