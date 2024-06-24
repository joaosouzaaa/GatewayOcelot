using GatewayOcelot.API.Entities;

namespace GatewayOcelot.API.DataTransferObjects.Product;

public sealed record ProductResponse(
    Guid Id, 
    string Name, 
    string Description, 
    decimal Price, 
    DateTime ManufacturedDate, 
    DateTime CreatedDate, 
    Address Address);
