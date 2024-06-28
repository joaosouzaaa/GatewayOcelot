using MongoDB.Bson.Serialization;

namespace GatewayOcelot.Products.API.Infrastructure.EntitiesMappings.BaseMappings;

public abstract class EntityMappingBase<TEntity>
{
    public void Configure() =>
        BsonClassMap.RegisterClassMap<TEntity>(Map);

    protected abstract void Map(BsonClassMap<TEntity> classMap);
}
