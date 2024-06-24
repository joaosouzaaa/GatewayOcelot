using GatewayOcelot.API.Interfaces.Data.DatabaseContexts;
using GatewayOcelot.API.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GatewayOcelot.API.Data.DatabaseContexts;

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
