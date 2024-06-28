using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Infrastructure.EntitiesMappings.BaseMappings;
using MongoDB.Bson.Serialization;

namespace GatewayOcelot.Products.API.Infrastructure.EntitiesMappings;

public sealed class ProductMapping : EntityMappingBase<Product>
{
    protected override void Map(BsonClassMap<Product> classMap)
    {
        classMap.AutoMap();

        classMap.MapIdField(p => p.Id);
    }
}
