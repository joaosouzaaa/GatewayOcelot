using GatewayOcelot.Products.API.DataTransferObjects.Address;
using GatewayOcelot.Products.API.Entities;

namespace GatewayOcelot.Products.API.Interfaces.Mappers;

public interface IAddressMapper
{
    Address ApiResponseAndRequestToDomain(AddressApiResponse addressApiResponse, AddressRequest addressRequest);
    AddressResponse DomainToResponse(Address address);
}
