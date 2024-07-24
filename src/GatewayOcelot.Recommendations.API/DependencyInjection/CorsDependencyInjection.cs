using GatewayOcelot.Recommendations.API.Constants;

namespace GatewayOcelot.Recommendations.API.DependencyInjection;

internal static class CorsDependencyInjection
{
    internal static void AddCorsDependencyInjection(this IServiceCollection services) =>
        services.AddCors(options =>
            options.AddPolicy(CorsNamesConstants.CorsPolicy, builder =>
                builder.SetIsOriginAllowed(origin => true)
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()));
}