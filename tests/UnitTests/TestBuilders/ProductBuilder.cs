using GatewayOcelot.API.DataTransferObjects.Address;
using GatewayOcelot.API.DataTransferObjects.Product;
using GatewayOcelot.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class ProductBuilder
{
    private readonly Guid _id = Guid.NewGuid();
    private string _name = "test";
    private string _description = "randomword1";
    private decimal _price = 123;
    private readonly DateTime _manufacturedDate = DateTime.UtcNow;
    private readonly DateTime _createdDate = DateTime.UtcNow;
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
            CreatedDate = _createdDate,
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
            _createdDate,
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
