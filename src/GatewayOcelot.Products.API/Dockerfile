FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GatewayOcelot.Products.API/GatewayOcelot.Products.API.csproj", "src/GatewayOcelot.Products.API/"]
RUN dotnet restore "./src/GatewayOcelot.Products.API/GatewayOcelot.Products.API.csproj"
COPY . .
WORKDIR "/src/src/GatewayOcelot.Products.API"
RUN dotnet build "./GatewayOcelot.Products.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GatewayOcelot.Products.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewayOcelot.Products.API.dll"]