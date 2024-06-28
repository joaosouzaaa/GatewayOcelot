namespace GatewayOcelot.Recommendations.API.Entities;

public sealed class Recommendation
{
    public Guid Id { get; set; }
    public required Guid ProductId { get; set; }
}
