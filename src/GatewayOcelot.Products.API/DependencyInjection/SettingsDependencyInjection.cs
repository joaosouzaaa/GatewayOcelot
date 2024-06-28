using FluentValidation;
using GatewayOcelot.Products.API.Filters;
using GatewayOcelot.Products.API.Interfaces.Settings;
using GatewayOcelot.Products.API.Settings.NotificationSettings;
using System.Reflection;

namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<INotificationHandler, NotificationHandler>();

        services.AddScoped<NotificationFilter>();
    }
}
