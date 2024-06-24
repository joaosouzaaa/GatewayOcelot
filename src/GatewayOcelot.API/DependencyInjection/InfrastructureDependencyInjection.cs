using GatewayOcelot.API.Data.DatabaseContexts;
using GatewayOcelot.API.Data.Repositories;
using GatewayOcelot.API.Factories;
using GatewayOcelot.API.Interfaces.Data.DatabaseContexts;
using GatewayOcelot.API.Interfaces.Data.Repositories;

namespace GatewayOcelot.API.DependencyInjection;

internal static class InfrastructureDependencyInjection
{
    internal static void AddInfrastructureDependencyInjection(this IServiceCollection services)
    {
        MappingFactory.ConfigureMongoDbMappings();

        services.AddScoped<IMongoDbContext, MongoDbContext>();

        services.AddScoped<IProductRepository, ProductRepository>();
    }
}
