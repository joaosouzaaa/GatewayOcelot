using GatewayOcelot.Recommendations.API.Entities;
using GatewayOcelot.Recommendations.API.Infrastructure.DatabaseContexts;
using GatewayOcelot.Recommendations.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GatewayOcelot.Recommendations.API.Infrastructure.Repositories;

public sealed class RecommendationRepository(RecommendationsDbContext dbContext) : IRecommendationRepository, IDisposable
{
    private DbSet<Recommendation> DbContextSet => dbContext.Set<Recommendation>();

    public Task AddAsync(Recommendation recommendation)
    {
        DbContextSet.Add(recommendation);

        return dbContext.SaveChangesAsync();
    }

    public Task<List<Recommendation>> GetAllAsync()
    {
        const int recommendationsCount = 5;

        return DbContextSet.OrderBy(e => Guid.NewGuid())
                           .Take(recommendationsCount)
                           .ToListAsync();
    }

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}
