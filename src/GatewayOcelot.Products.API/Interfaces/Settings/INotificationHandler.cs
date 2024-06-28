using GatewayOcelot.Products.API.Settings.NotificationSettings;

namespace GatewayOcelot.Products.API.Interfaces.Settings;

public interface INotificationHandler
{
    bool HasNotifications();
    List<Notification> GetNotifications();
    void AddNotification(string key, string message);
}
