using GatewayOcelot.API.Entities;

namespace GatewayOcelot.API.DataTransferObjects.Product;

public sealed record ProductCreateRequest(
    string Name, 
    string Description, 
    decimal Price, 
    DateTime ManufacturedDate, 
    Address Address);
