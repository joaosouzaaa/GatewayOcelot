FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GatewayOcelot.Recommendations.API/GatewayOcelot.Recommendations.API.csproj", "src/GatewayOcelot.Recommendations.API/"]
RUN dotnet restore "./src/GatewayOcelot.Recommendations.API/GatewayOcelot.Recommendations.API.csproj"
COPY . .
WORKDIR "/src/src/GatewayOcelot.Recommendations.API"
RUN dotnet build "./GatewayOcelot.Recommendations.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GatewayOcelot.Recommendations.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewayOcelot.Recommendations.API.dll"]