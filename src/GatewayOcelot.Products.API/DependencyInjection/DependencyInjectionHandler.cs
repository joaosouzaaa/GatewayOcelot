namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();
        services.AddOptionsDependencyInjection(configuration);
        services.AddInfrastructureDependencyInjection();
        services.AddSettingsDependencyInjection();
        services.AddHttpClientDependencyInjection(configuration);
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
    }
}
