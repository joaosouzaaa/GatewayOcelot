namespace GatewayOcelot.Products.API.Options;

public sealed class MongoDBOptions
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}
