using GatewayOcelot.Products.API.Interfaces.Infrastructure.DatabaseContexts;
using GatewayOcelot.Products.API.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GatewayOcelot.Products.API.Infrastructure.DatabaseContexts;

public sealed class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDBOptions> mongoDbOptions)
    {
        var client = new MongoClient(mongoDbOptions.Value.ConnectionString);

        _database = client.GetDatabase(mongoDbOptions.Value.DatabaseName);
    }

    public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) =>
        _database.GetCollection<TEntity>(collectionName);
}
