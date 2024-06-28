using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Infrastructure.EntitiesMappings.BaseMappings;
using MongoDB.Bson.Serialization;

namespace GatewayOcelot.Products.API.Infrastructure.EntitiesMappings;

public sealed class AddressMapping : EntityMappingBase<Address>
{
    protected override void Map(BsonClassMap<Address> classMap)
    {
        classMap.AutoMap();
    }
}
