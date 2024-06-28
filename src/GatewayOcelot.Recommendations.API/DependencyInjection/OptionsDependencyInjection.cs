using GatewayOcelot.Recommendations.API.Constants;
using GatewayOcelot.Recommendations.API.Options;

namespace GatewayOcelot.Recommendations.API.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQOptions>(configuration.GetSection(OptionsConstants.RabbitMQSection));
    }
}
