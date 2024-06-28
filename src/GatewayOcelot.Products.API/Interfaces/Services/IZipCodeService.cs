using GatewayOcelot.Products.API.DataTransferObjects.Address;
using GatewayOcelot.Products.API.Entities;

namespace GatewayOcelot.Products.API.Interfaces.Services;

public interface IZipCodeService
{
    Task<Address?> GetAddressByRequestAsync(AddressRequest addressRequest);
}
