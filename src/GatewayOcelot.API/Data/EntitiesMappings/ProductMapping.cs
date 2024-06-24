using GatewayOcelot.API.Data.EntitiesMappings.BaseMappings;
using GatewayOcelot.API.Entities;
using MongoDB.Bson.Serialization;

namespace GatewayOcelot.API.Data.EntitiesMappings;

public sealed class ProductMapping : EntityMappingBase<Product>
{
    protected override void Map(BsonClassMap<Product> classMap)
    {
        classMap.AutoMap();

        classMap.MapIdField(p => p.Id);
    }
}
