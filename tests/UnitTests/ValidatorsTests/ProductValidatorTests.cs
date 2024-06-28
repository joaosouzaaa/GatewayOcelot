using GatewayOcelot.Products.API.Validators;
using UnitTests.TestBuilders;

namespace UnitTests.ValidatorsTests;

public sealed class ProductValidatorTests
{
    private readonly ProductValidator _productValidator;

    public ProductValidatorTests()
    {
        _productValidator = new ProductValidator();
    }

    [Fact]
    public async Task ValidateAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var productToValidate = ProductBuilder.NewObject().DomainBuild();

        // A
        var validationResult = await _productValidator.ValidateAsync(productToValidate);

        // A
        Assert.True(validationResult.IsValid);
    }

    [Theory]
    [MemberData(nameof(InvalidNameParameters))]
    public async Task ValidateAsync_InvalidName_ReturnsFalse(string name)
    {
        // A
        var productWithInvalidName = ProductBuilder.NewObject().WithName(name).DomainBuild();

        // A
        var validationResult = await _productValidator.ValidateAsync(productWithInvalidName);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidNameParameters() =>
        new()
        {
            "",
            new string('a', 151)
        };

    [Theory]
    [MemberData(nameof(InvalidDescriptionParameters))]
    public async Task ValidateAsync_InvalidDescription_ReturnsFalse(string description)
    {
        // A
        var productWithInvalidDescription = ProductBuilder.NewObject().WithDescription(description).DomainBuild();

        // A
        var validationResult = await _productValidator.ValidateAsync(productWithInvalidDescription);

        // A
        Assert.False(validationResult.IsValid);
    }

    public static TheoryData<string> InvalidDescriptionParameters() =>
        new()
        {
            "",
            "test",
            new string('a', 2001)
        };

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task ValidateAsync_InvalidPrice_ReturnsFalse(decimal price)
    {
        // A
        var productWithInvalidPrice = ProductBuilder.NewObject().WithPrice(price).DomainBuild();

        // A
        var validationResult = await _productValidator.ValidateAsync(productWithInvalidPrice);

        // A
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_InvalidAddress_ReturnsFalse()
    {
        // A
        var invalidAddress = AddressBuilder.NewObject().WithZipCode("tes").DomainBuild();
        var productWithInvalidInvalidAddress = ProductBuilder.NewObject().WithAddress(invalidAddress).DomainBuild();

        // A
        var validationResult = await _productValidator.ValidateAsync(productWithInvalidInvalidAddress);

        // A
        Assert.False(validationResult.IsValid);
    }
}
