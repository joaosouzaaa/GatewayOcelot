using Microsoft.EntityFrameworkCore;

namespace GatewayOcelot.Recommendations.API.Infrastructure.DatabaseContexts;

public sealed class RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecommendationsDbContext).Assembly);
}
