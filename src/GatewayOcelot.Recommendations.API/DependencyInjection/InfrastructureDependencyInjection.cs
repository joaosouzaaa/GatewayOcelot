using GatewayOcelot.Recommendations.API.Infrastructure.Consumers;
using GatewayOcelot.Recommendations.API.Infrastructure.DatabaseContexts;
using GatewayOcelot.Recommendations.API.Infrastructure.Repositories;
using GatewayOcelot.Recommendations.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GatewayOcelot.Recommendations.API.DependencyInjection;

internal static class InfrastructureDependencyInjection
{
    internal static void AddInfrastructureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RecommendationsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IRecommendationRepository, RecommendationRepository>();

        services.AddHostedService<ProductCreatedConsumer>();
    }
}
