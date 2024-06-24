using GatewayOcelot.API.Data.EntitiesMappings.BaseMappings;
using GatewayOcelot.API.Entities;
using MongoDB.Bson.Serialization;

namespace GatewayOcelot.API.Data.EntitiesMappings;

public sealed class AddressMapping : EntityMappingBase<Address>
{
    protected override void Map(BsonClassMap<Address> classMap)
    {
        classMap.AutoMap();
    }
}
