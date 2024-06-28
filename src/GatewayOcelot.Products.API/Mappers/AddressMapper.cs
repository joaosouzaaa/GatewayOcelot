using GatewayOcelot.Products.API.DataTransferObjects.Address;
using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Interfaces.Mappers;

namespace GatewayOcelot.Products.API.Mappers;

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
