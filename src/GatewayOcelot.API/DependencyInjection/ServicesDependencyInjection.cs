using GatewayOcelot.API.Interfaces.Services;
using GatewayOcelot.API.Services;

namespace GatewayOcelot.API.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services) =>
        services.AddScoped<IProductService, ProductService>();
}
