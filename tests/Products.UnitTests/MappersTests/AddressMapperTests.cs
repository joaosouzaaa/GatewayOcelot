using GatewayOcelot.Products.API.Mappers;
using Products.UnitTests.TestBuilders;

namespace Products.UnitTests.MappersTests;

public sealed class AddressMapperTests
{
    private readonly AddressMapper _addressMapper;

    public AddressMapperTests()
    {
        _addressMapper = new AddressMapper();
    }

    [Fact]
    public void ApiResponseAndRequestToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var addressApiResponse = AddressBuilder.NewObject().ApiResponseBuild();
        var addressRequest = AddressBuilder.NewObject().RequestBuild();

        // A
        var addressResult = _addressMapper.ApiResponseAndRequestToDomain(addressApiResponse, addressRequest);

        // A
        Assert.Equal(addressResult.City, addressApiResponse.Localidade);
        Assert.Equal(addressResult.Complement, addressRequest.Complement);
        Assert.Equal(addressResult.District, addressApiResponse.Bairro);
        Assert.Equal(addressResult.Number, addressRequest.Number);
        Assert.Equal(addressResult.State, addressApiResponse.Uf);
        Assert.Equal(addressResult.Street, addressApiResponse.Logradouro);
        Assert.Equal(addressResult.ZipCode, addressRequest.ZipCode);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var address = AddressBuilder.NewObject().DomainBuild();

        // A
        var addressResponseResult = _addressMapper.DomainToResponse(address);

        // A
        Assert.Equal(addressResponseResult.ZipCode, addressResponseResult.ZipCode);
        Assert.Equal(addressResponseResult.Number, addressResponseResult.Number);
        Assert.Equal(addressResponseResult.Street, addressResponseResult.Street);
        Assert.Equal(addressResponseResult.City, addressResponseResult.City);
        Assert.Equal(addressResponseResult.State, addressResponseResult.State);
        Assert.Equal(addressResponseResult.District, addressResponseResult.District);
        Assert.Equal(addressResponseResult.Complement, addressResponseResult.Complement);
    }
}
