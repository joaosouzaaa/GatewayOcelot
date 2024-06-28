namespace GatewayOcelot.Products.API.Extensions;

public static class StringFormatExtensions
{
    public static string FormatTo(this string message, params object[] args) =>
        string.Format(message, args);
}
