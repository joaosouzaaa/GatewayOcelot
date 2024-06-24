namespace GatewayOcelot.API.Entities;

public sealed class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required DateTime ManufacturedDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public required Address Address { get; set; }
}
