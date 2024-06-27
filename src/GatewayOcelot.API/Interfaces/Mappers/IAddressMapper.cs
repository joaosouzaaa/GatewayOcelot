using GatewayOcelot.API.DataTransferObjects.Address;
using GatewayOcelot.API.Entities;

namespace GatewayOcelot.API.Interfaces.Mappers;

public interface IAddressMapper
{
    Address ApiResponseAndRequestToDomain(AddressApiResponse addressApiResponse, AddressRequest addressRequest);
    AddressResponse DomainToResponse(Address address);
}
