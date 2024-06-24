using GatewayOcelot.API.Interfaces.Settings;

namespace GatewayOcelot.API.Settings.NotificationSettings;

public sealed class NotificationHandler : INotificationHandler
{
    private readonly List<Notification> _notifications;

    public NotificationHandler() => _notifications = [];

    public bool HasNotifications() =>
        _notifications.Any();

    public List<Notification> GetNotifications() =>
        _notifications;

    public void AddNotification(string key, string message) =>
        _notifications.Add(new(key, message));
}