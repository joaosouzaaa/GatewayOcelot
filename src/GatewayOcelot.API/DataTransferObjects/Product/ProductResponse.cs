using GatewayOcelot.API.DataTransferObjects.Address;

namespace GatewayOcelot.API.DataTransferObjects.Product;

public sealed record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    DateTime ManufacturedDate,
    AddressResponse Address);
