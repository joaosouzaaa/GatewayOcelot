using GatewayOcelot.API.DataTransferObjects.Address;
using GatewayOcelot.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class AddressBuilder
{
    private string _zipCode = "29313352";
    private string _number = "123";
    private string? _complement = "test";
    private readonly string _city = "test";
    private readonly string _district = "test";
    private readonly string _state = "tr";
    private readonly string _street = "rand";

    public static AddressBuilder NewObject() =>
        new();

    public Address DomainBuild() =>
        new()
        {
            ZipCode = _zipCode,
            Number = _number,
            Complement = _complement,
            City = _city,
            District = _district,
            State = _state,
            Street = _street
        };

    public AddressRequest RequestBuild() =>
        new(_zipCode,
            _number,
            _complement);

    public AddressApiResponse ApiResponseBuild() =>
        new(_street,
            _district,
            _city,
            _state);

    public AddressResponse ResponseBuild() =>
        new(_zipCode,
            _number,
            _street,
            _city,
            _state,
            _district,
            _complement);

    public AddressBuilder WithZipCode(string zipCode)
    {
        _zipCode = zipCode;

        return this;
    }

    public AddressBuilder WithNumber(string number)
    {
        _number = number;

        return this;
    }

    public AddressBuilder WithComplement(string? complement)
    {
        _complement = complement;

        return this;
    }
}
