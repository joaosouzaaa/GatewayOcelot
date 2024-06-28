using GatewayOcelot.Products.API.Interfaces.Mappers;
using GatewayOcelot.Products.API.Mappers;

namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAddressMapper, AddressMapper>();
        services.AddScoped<IProductMapper, ProductMapper>();
    }
}
