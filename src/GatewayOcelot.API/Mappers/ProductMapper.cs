﻿using GatewayOcelot.API.DataTransferObjects.Product;
using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Interfaces.Mappers;
using GatewayOcelot.API.Settings.PaginationSettings;

namespace GatewayOcelot.API.Mappers;

public sealed class ProductMapper : IProductMapper
{
    public Product CreateToDomain(ProductCreateRequest productCreateRequest) =>
        new()
        {
            Name = productCreateRequest.Name,
            Description = productCreateRequest.Description,
            Price = productCreateRequest.Price,
            ManufacturedDate = productCreateRequest.ManufacturedDate,
            CreatedDate = DateTime.UtcNow,
            Address = productCreateRequest.Address
        };

    public Product UpdateToDomain(ProductUpdateRequest productUpdateRequest) =>
        new()
        {
            Id = productUpdateRequest.Id,
            Name = productUpdateRequest.Name,
            Description = productUpdateRequest.Description,
            Price = productUpdateRequest.Price,
            ManufacturedDate = productUpdateRequest.ManufacturedDate,
            Address = productUpdateRequest.Address
        };

    public ProductResponse DomainToResponse(Product product) =>
        new(product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.ManufacturedDate,
            product.CreatedDate,
            product.Address);

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
