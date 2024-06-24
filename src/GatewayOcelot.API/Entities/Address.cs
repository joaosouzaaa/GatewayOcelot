namespace GatewayOcelot.API.Entities;

public sealed class Address
{
    public required string ZipCode { get; set; }
    public required string Number { get; set; }
    public string? Complement { get; set; }
}
