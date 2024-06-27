using GatewayOcelot.API.DataTransferObjects.Address;
using GatewayOcelot.API.Entities;

namespace GatewayOcelot.API.Interfaces.Services;

public interface IZipCodeService
{
    Task<Address?> GetAddressByRequestAsync(AddressRequest addressRequest);
}
