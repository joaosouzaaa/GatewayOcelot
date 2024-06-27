using GatewayOcelot.API.DataTransferObjects.Address;
using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Interfaces.Mappers;

namespace GatewayOcelot.API.Mappers;

public sealed class AddressMapper : IAddressMapper
{
    public Address ApiResponseAndRequestToDomain(AddressApiResponse addressApiResponse, AddressRequest addressRequest) =>
        new()
        {
            City = addressApiResponse.Localidade,
            Complement = addressRequest.Complement,
            District = addressApiResponse.Bairro,
            Number = addressRequest.Number,
            State = addressApiResponse.Uf,
            Street = addressApiResponse.Logradouro,
            ZipCode = addressRequest.ZipCode
        };

    public AddressResponse DomainToResponse(Address address) =>
        new(address.ZipCode,
            address.Number,
            address.Street,
            address.City,
            address.State,
            address.District,
            address.Complement);
}
