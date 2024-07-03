using GatewayOcelot.Products.API.DataTransferObjects.Address;
using GatewayOcelot.Products.API.DataTransferObjects.Product;
using GatewayOcelot.Products.API.Entities;

namespace Products.UnitTests.TestBuilders;

public sealed class ProductBuilder
{
    private readonly Guid _id = Guid.NewGuid();
    private string _name = "test";
    private string _description = "randomword1";
    private decimal _price = 123;
    private readonly DateTime _manufacturedDate = DateTime.UtcNow;
    private Address _address = AddressBuilder.NewObject().DomainBuild();
    private readonly AddressRequest _addressRequest = AddressBuilder.NewObject().RequestBuild();

    public static ProductBuilder NewObject() =>
        new();

    public Product DomainBuild() =>
        new()
        {
            Id = _id,
            Name = _name,
            Description = _description,
            Price = _price,
            ManufacturedDate = _manufacturedDate,
            Address = _address
        };

    public ProductCreateRequest CreateRequestBuild() =>
        new(_name,
            _description,
            _price,
            _manufacturedDate,
            _addressRequest);

    public ProductUpdateRequest UpdateRequestBuild() =>
        new(_id,
            _name,
            _description,
            _price,
            _manufacturedDate,
            _addressRequest);

    public ProductResponse ResponseBuild() =>
        new(_id,
            _name,
            _description,
            _price,
            _manufacturedDate,
            AddressBuilder.NewObject().ResponseBuild());

    public ProductBuilder WithName(string name)
    {
        _name = name;

        return this;
    }

    public ProductBuilder WithDescription(string description)
    {
        _description = description;

        return this;
    }

    public ProductBuilder WithPrice(decimal price)
    {
        _price = price;

        return this;
    }

    public ProductBuilder WithAddress(Address address)
    {
        _address = address;

        return this;
    }
}
