using GatewayOcelot.Products.API.DataTransferObjects.Product;
using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Interfaces.Mappers;
using GatewayOcelot.Products.API.Settings.PaginationSettings;

namespace GatewayOcelot.Products.API.Mappers;

public sealed class ProductMapper(IAddressMapper addressMapper) : IProductMapper
{
    public Product CreateToDomain(ProductCreateRequest productCreateRequest) =>
        new()
        {
            Name = productCreateRequest.Name,
            Description = productCreateRequest.Description,
            Price = productCreateRequest.Price,
            ManufacturedDate = productCreateRequest.ManufacturedDate
        };

    public Product UpdateToDomain(ProductUpdateRequest productUpdateRequest) =>
        new()
        {
            Id = productUpdateRequest.Id,
            Name = productUpdateRequest.Name,
            Description = productUpdateRequest.Description,
            Price = productUpdateRequest.Price,
            ManufacturedDate = productUpdateRequest.ManufacturedDate
        };

    public ProductResponse DomainToResponse(Product product) =>
        new(product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.ManufacturedDate,
            addressMapper.DomainToResponse(product.Address));

    public PageList<ProductResponse> DomainPageListToResponsePageList(PageList<Product> productPageList) =>
        new()
        {
            CurrentPage = productPageList.CurrentPage,
            Data = productPageList.Data.Select(DomainToResponse).ToList(),
            PageSize = productPageList.PageSize,
            TotalCount = productPageList.TotalCount,
            TotalPages = productPageList.TotalPages
        };
}
