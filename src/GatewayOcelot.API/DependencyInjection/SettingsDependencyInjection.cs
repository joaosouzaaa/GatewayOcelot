using FluentValidation;
using GatewayOcelot.API.Filters;
using GatewayOcelot.API.Interfaces.Settings;
using GatewayOcelot.API.Settings.NotificationSettings;
using System.Reflection;

namespace GatewayOcelot.API.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<INotificationHandler, NotificationHandler>();

        services.AddScoped<NotificationFilter>();
    }
}
