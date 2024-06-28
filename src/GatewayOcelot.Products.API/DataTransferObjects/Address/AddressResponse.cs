namespace GatewayOcelot.Products.API.DataTransferObjects.Address;

public sealed record AddressResponse(
    string ZipCode,
    string Number,
    string Street,
    string City,
    string State,
    string District,
    string? Complement);
