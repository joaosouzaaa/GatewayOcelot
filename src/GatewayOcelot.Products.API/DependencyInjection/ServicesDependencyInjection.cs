using GatewayOcelot.Products.API.Interfaces.Services;
using GatewayOcelot.Products.API.Services;

namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IZipCodeService, ZipCodeService>();
    }
}
