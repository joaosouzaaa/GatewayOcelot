using GatewayOcelot.Recommendations.API.Entities;

namespace GatewayOcelot.Recommendations.API.Interfaces.Repositories;

public interface IRecommendationRepository
{
    Task AddAsync(Recommendation recommendation);
    Task<List<Recommendation>> GetAllAsync();
}
