using GatewayOcelot.Recommendations.API.Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace GatewayOcelot.Recommendations.API.DependencyInjection;

internal static class MigrationHandler
{
    internal static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var healthCheckDbContext = scope.ServiceProvider.GetRequiredService<RecommendationsDbContext>();

        try
        {
            healthCheckDbContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
