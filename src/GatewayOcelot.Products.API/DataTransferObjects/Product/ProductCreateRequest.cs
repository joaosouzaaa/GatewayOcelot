using GatewayOcelot.Products.API.DataTransferObjects.Address;

namespace GatewayOcelot.Products.API.DataTransferObjects.Product;

public sealed record ProductCreateRequest(
    string Name,
    string Description,
    decimal Price,
    DateTime ManufacturedDate,
    AddressRequest Address);
