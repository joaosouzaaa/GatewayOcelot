﻿using GatewayOcelot.Products.API.Constants;

namespace GatewayOcelot.Products.API.DependencyInjection;

internal static class CorsDependencyInjection
{
    internal static void AddCorsDependencyInjection(this IServiceCollection services) =>

        services.AddCors(options =>
            options.AddPolicy(CorsNamesConstants.CorsPolicy, builder =>
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .SetIsOriginAllowed(origin => true)
                       .AllowCredentials()));
}