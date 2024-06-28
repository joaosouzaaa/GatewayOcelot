using GatewayOcelot.Recommendations.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GatewayOcelot.Recommendations.API.Infrastructure.EntitiesMapping;

internal sealed class RecommendationsMapping : IEntityTypeConfiguration<Recommendation>
{
    public void Configure(EntityTypeBuilder<Recommendation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ProductId)
               .IsRequired(true)
               .HasColumnName(nameof(Recommendation.ProductId));
    }
}
