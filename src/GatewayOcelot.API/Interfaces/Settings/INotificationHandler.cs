using GatewayOcelot.API.Settings.NotificationSettings;

namespace GatewayOcelot.API.Interfaces.Settings;

public interface INotificationHandler
{
    bool HasNotifications();
    List<Notification> GetNotifications();
    void AddNotification(string key, string message);
}
