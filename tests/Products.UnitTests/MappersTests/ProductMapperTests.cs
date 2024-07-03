using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Interfaces.Mappers;
using GatewayOcelot.Products.API.Mappers;
using GatewayOcelot.Products.API.Settings.PaginationSettings;
using Moq;
using Products.UnitTests.TestBuilders;

namespace Products.UnitTests.MappersTests;

public sealed class ProductMapperTests
{
    private readonly Mock<IAddressMapper> _addressMapperMock;
    private readonly ProductMapper _productMapper;

    public ProductMapperTests()
    {
        _addressMapperMock = new Mock<IAddressMapper>();
        _productMapper = new ProductMapper(_addressMapperMock.Object);
    }

    [Fact]
    public void CreateToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var productCreateRequest = ProductBuilder.NewObject().CreateRequestBuild();

        // A
        var productResult = _productMapper.CreateToDomain(productCreateRequest);

        // A
        Assert.Equal(productResult.Name, productCreateRequest.Name);
        Assert.Equal(productResult.Description, productCreateRequest.Description);
        Assert.Equal(productResult.Price, productCreateRequest.Price);
        Assert.Equal(productResult.ManufacturedDate, productCreateRequest.ManufacturedDate);
        Assert.Null(productResult.Address);
    }

    [Fact]
    public void UpdateToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();

        // A
        var productResult = _productMapper.UpdateToDomain(productUpdateRequest);

        // A
        Assert.Equal(productResult.Id, productUpdateRequest.Id);
        Assert.Equal(productResult.Name, productUpdateRequest.Name);
        Assert.Equal(productResult.Description, productUpdateRequest.Description);
        Assert.Equal(productResult.Price, productUpdateRequest.Price);
        Assert.Equal(productResult.ManufacturedDate, productUpdateRequest.ManufacturedDate);
        Assert.Null(productResult.Address);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var product = ProductBuilder.NewObject().DomainBuild();

        var addressResponse = AddressBuilder.NewObject().ResponseBuild();
        _addressMapperMock.Setup(a => a.DomainToResponse(It.IsAny<Address>()))
            .Returns(addressResponse);

        // A
        var productResponseResult = _productMapper.DomainToResponse(product);

        // A
        Assert.Equal(productResponseResult.Id, product.Id);
        Assert.Equal(productResponseResult.Name, product.Name);
        Assert.Equal(productResponseResult.Description, product.Description);
        Assert.Equal(productResponseResult.Price, product.Price);
        Assert.Equal(productResponseResult.ManufacturedDate, product.ManufacturedDate);
        Assert.Equal(productResponseResult.Address, addressResponse);
    }

    [Fact]
    public void DomainPageListToResponsePageList_SuccessfulScenario_ReturnsResponsePageListObject()
    {
        // A
        var productPageList = new PageList<Product>()
        {
            CurrentPage = 123,
            Data =
            [
                ProductBuilder.NewObject().DomainBuild(),
                ProductBuilder.NewObject().DomainBuild()
            ],
            PageSize = 123,
            TotalCount = 9343945,
            TotalPages = 2
        };

        var addressResponse = AddressBuilder.NewObject().ResponseBuild();
        _addressMapperMock.SetupSequence(a => a.DomainToResponse(It.IsAny<Address>()))
            .Returns(addressResponse)
            .Returns(addressResponse);

        // A
        var productResponsePageListResult = _productMapper.DomainPageListToResponsePageList(productPageList);

        // A
        Assert.Equal(productResponsePageListResult.CurrentPage, productPageList.CurrentPage);
        Assert.Equal(productResponsePageListResult.Data.Count, productPageList.Data.Count);
        Assert.Equal(productResponsePageListResult.PageSize, productPageList.PageSize);
        Assert.Equal(productResponsePageListResult.TotalCount, productPageList.TotalCount);
        Assert.Equal(productResponsePageListResult.TotalPages, productPageList.TotalPages);
    }
}
