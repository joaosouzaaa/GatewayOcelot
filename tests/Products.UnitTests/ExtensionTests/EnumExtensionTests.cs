using GatewayOcelot.Products.API.Enums;
using GatewayOcelot.Products.API.Extensions;

namespace UnitTests.ExtensionTests;

public sealed class EnumExtensionTests
{
    [Fact]
    public void Description_SuccessfulScenario_ReturnsEnumDescription()
    {
        // A
        var notificationMessages = ENotificationMessages.NotFound;

        const string expectedDescription = "{0} not found.";

        // A
        var descriptionResult = notificationMessages.Description();

        // A
        Assert.Equal(expectedDescription, descriptionResult);
    }
}
