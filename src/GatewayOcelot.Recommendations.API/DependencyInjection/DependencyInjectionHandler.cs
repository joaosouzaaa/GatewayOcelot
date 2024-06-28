namespace GatewayOcelot.Recommendations.API.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();
        services.AddOptionsDependencyInjection(configuration);
        services.AddInfrastructureDependencyInjection(configuration);
    }
}
