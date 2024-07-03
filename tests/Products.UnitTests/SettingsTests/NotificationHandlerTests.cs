using GatewayOcelot.Products.API.Settings.NotificationSettings;

namespace Products.UnitTests.SettingsTests;

public sealed class NotificationHandlerTests
{
    private readonly NotificationHandler _notificationHandler;

    public NotificationHandlerTests()
    {
        _notificationHandler = new NotificationHandler();
    }

    [Fact]
    public void GetNotifications_AddNotifications_ListHasNotifications()
    {
        // A
        const int notificationCount = 2;
        for (var i = 0; i < notificationCount; i++)
        {
            _notificationHandler.AddNotification("test", "test");
        }

        // A
        var notificationListResult = _notificationHandler.GetNotifications();

        // A
        Assert.Equal(notificationCount, notificationListResult.Count);
    }

    [Fact]
    public void HasNotifications_AddNotification_HasNotificationTrue()
    {
        // A
        _notificationHandler.AddNotification("test", "test1");

        // A
        var hasNotificationsResult = _notificationHandler.HasNotifications();

        // A
        Assert.True(hasNotificationsResult);
    }

    [Fact]
    public void HasNotifications_HasNotificationFalse()
    {
        var hasNotificationsResult = _notificationHandler.HasNotifications();

        Assert.False(hasNotificationsResult);
    }
}
