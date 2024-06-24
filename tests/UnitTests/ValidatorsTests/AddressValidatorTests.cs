using GatewayOcelot.API.Validators;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorsTests;

public sealed class AddressValidatorTests
{
    private readonly AddressValidator _addressValidator;

    public AddressValidatorTests()
    {
        _addressValidator = new AddressValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var addressToValidate = AddressBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _addressValidator.ValidateAsync(addressToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidZipCodeParameters))]
    public async Task ValidateAsync_InvalidZipCode_ReturnsFalse(string zipCode)
    {
        // A
        var addressWithInvalidZipCode = AddressBuilder.NewObject().WithZipCode(zipCode).DomainBuild();

        // A
        var validationResult = await _addressValidator.ValidateAsync(addressWithInvalidZipCode);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidZipCodeParameters() =>
        new()
        {
            "",
            "trest",
            "random",
            "randomworda",
            "123455667765434",
            "123123"
        };

    [Theory]
    [MemberData(nameof(InvalidNumberParameters))]
    public async Task ValidateAsync_InvalidNumber_ReturnsFalse(string number)
    {
        // A
        var addressWithInvalidNumber = AddressBuilder.NewObject().WithNumber(number).DomainBuild();

        // A
        var validationResult = await _addressValidator.ValidateAsync(addressWithInvalidNumber);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNumberParameters() =>
        new()
        {
            "",
            "randomworda12312",
            "123455667765434123123"
        };

    [Fact]
    public async Task ValidateAsync_InvalidComplement_ReturnsFalse()
    {
        // A
        var invalidComplement = new string('a', 102);
        var addressWithInvalidComplement = AddressBuilder.NewObject().WithComplement(invalidComplement).DomainBuild();

        // A
        var validationResult = await _addressValidator.ValidateAsync(addressWithInvalidComplement);

        // A
        Assert.False(validationResult.IsValid);
    }
}
