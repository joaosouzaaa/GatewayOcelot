using GatewayOcelot.Products.API.Constants;
using GatewayOcelot.Products.API.Options;

namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDBOptions>(configuration.GetSection(OptionsConstants.MongoDBSection));
        services.Configure<RabbitMQOptions>(configuration.GetSection(OptionsConstants.RabbitMQSection));
    }
}
