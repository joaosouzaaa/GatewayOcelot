FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GatewayOcelot.Gateway/GatewayOcelot.Gateway.csproj", "src/GatewayOcelot.Gateway/"]
RUN dotnet restore "./src/GatewayOcelot.Gateway/GatewayOcelot.Gateway.csproj"
COPY . .
WORKDIR "/src/src/GatewayOcelot.Gateway"
RUN dotnet build "./GatewayOcelot.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GatewayOcelot.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewayOcelot.Gateway.dll"]