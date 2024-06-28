using GatewayOcelot.Products.API.DataTransferObjects.Address;

namespace GatewayOcelot.Products.API.DataTransferObjects.Product;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    DateTime ManufacturedDate,
    AddressResponse Address);
