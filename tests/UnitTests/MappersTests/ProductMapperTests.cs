using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Mappers;
using GatewayOcelot.API.Settings.PaginationSettings;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;

public sealed class ProductMapperTests
{
    private readonly ProductMapper _productMapper;

    public ProductMapperTests()
    {
        _productMapper = new ProductMapper();
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
        Assert.Equal(productResult.Address, productCreateRequest.Address);
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
        Assert.Equal(productResult.Address, productUpdateRequest.Address);
    }

    [Fact]
    public void DomainToResponse_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var product = ProductBuilder.NewObject().DomainBuild();

        // A
        var productResponseResult = _productMapper.DomainToResponse(product);

        // A
        Assert.Equal(productResponseResult.Id, product.Id);
        Assert.Equal(productResponseResult.Name, product.Name);
        Assert.Equal(productResponseResult.Description, product.Description);
        Assert.Equal(productResponseResult.Price, product.Price);
        Assert.Equal(productResponseResult.ManufacturedDate, product.ManufacturedDate);
        Assert.Equal(productResponseResult.CreatedDate, product.CreatedDate);
        Assert.Equal(productResponseResult.Address, product.Address);
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
