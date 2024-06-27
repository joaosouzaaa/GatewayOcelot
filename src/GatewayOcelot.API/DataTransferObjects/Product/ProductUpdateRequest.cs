using GatewayOcelot.API.DataTransferObjects.Address;

namespace GatewayOcelot.API.DataTransferObjects.Product;

public sealed record ProductUpdateRequest(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    DateTime ManufacturedDate,
    AddressRequest Address);
