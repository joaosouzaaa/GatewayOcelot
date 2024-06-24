using GatewayOcelot.API.Constants;
using GatewayOcelot.API.Options;

namespace GatewayOcelot.API.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration) =>

        services.Configure<MongoDBOptions>(configuration.GetSection(OptionsConstants.MongoDBSection));
}
