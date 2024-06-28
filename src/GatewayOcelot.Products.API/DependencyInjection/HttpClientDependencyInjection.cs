using GatewayOcelot.Products.API.Constants;

namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class HttpClientDependencyInjection
{
    internal static void AddHttpClientDependencyInjection(this IServiceCollection services, IConfiguration configuration) =>
        services.AddHttpClient(HttpClientNameConstants.ZipCodeHttpClient, options => options.BaseAddress = new Uri(configuration["ZipCodeHttpClient:BaseAddress"]!));
}
