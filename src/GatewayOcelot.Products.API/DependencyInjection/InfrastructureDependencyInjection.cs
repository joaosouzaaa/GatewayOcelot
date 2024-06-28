using GatewayOcelot.Products.API.Factories;
using GatewayOcelot.Products.API.Infrastructure.DatabaseContexts;
using GatewayOcelot.Products.API.Infrastructure.Publishers;
using GatewayOcelot.Products.API.Infrastructure.Repositories;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.DatabaseContexts;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.Publishers;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.Repositories;

namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class InfrastructureDependencyInjection
{
    internal static void AddInfrastructureDependencyInjection(this IServiceCollection services)
    {
        MappingFactory.ConfigureMongoDbMappings();

        services.AddScoped<IMongoDbContext, MongoDbContext>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IProductCreatedPublisher, ProductCreatedPublisher>();
    }
}
