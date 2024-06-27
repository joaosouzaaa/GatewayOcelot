using GatewayOcelot.API.Interfaces.Mappers;
using GatewayOcelot.API.Mappers;

namespace GatewayOcelot.API.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAddressMapper, AddressMapper>();
        services.AddScoped<IProductMapper, ProductMapper>();
    }
}
