using GatewayOcelot.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class AddressBuilder
{
    private string _zipCode = "12345678";
    private string _number = "123";
    private string? _complement = "test";

    public static AddressBuilder NewObject() =>
        new();

    public Address DomainBuild() =>
        new()
        {
            ZipCode = _zipCode,
            Number = _number,
            Complement = _complement
        };

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
