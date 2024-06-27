namespace GatewayOcelot.API.DataTransferObjects.Address;

public sealed record AddressRequest(
    string ZipCode, 
    string Number, 
    string? Complement);
