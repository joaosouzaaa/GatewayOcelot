﻿using FluentValidation;
using GatewayOcelot.API.DataTransferObjects.Product;
using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Interfaces.Data.Repositories;
using GatewayOcelot.API.Interfaces.Mappers;
using GatewayOcelot.API.Interfaces.Services;
using GatewayOcelot.API.Interfaces.Settings;
using GatewayOcelot.API.Settings.PaginationSettings;

namespace GatewayOcelot.API.Services;

public sealed class ProductService(
    IProductRepository productRepository,
    IProductMapper productMapper,
    IValidator<Product> validator,
    INotificationHandler notificationHandler)
    : IProductService
{
    public async Task AddAsync(ProductCreateRequest productCreateRequest)
    {
        var product = productMapper.CreateToDomain(productCreateRequest);

        if (!await IsValidAsync(product))
        {
            return;
        }

        await productRepository.InsertOneAsync(product);
    }

    public async Task UpdateAsync(ProductUpdateRequest productUpdateRequest)
    {
        if (!await productRepository.ExistsAsync(productUpdateRequest.Id))
        {
            notificationHandler.AddNotification("Not Found", "Product not found");

            return;
        }

        var product = productMapper.UpdateToDomain(productUpdateRequest);

        if (!await IsValidAsync(product))
        {
            return;
        }

        await productRepository.ReplaceOneAsync(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (!await productRepository.ExistsAsync(id))
        {
            notificationHandler.AddNotification("Not Found", "Product not found");

            return;
        }

        await productRepository.DeleteOneAsync(id);
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        return productMapper.DomainToResponse(product);
    }

    public async Task<PageList<ProductResponse>> GetAllPaginatedAsync(PageParameters pageParameters)
    {
        var productPageList = await productRepository.GetAllPaginatedAsync(pageParameters);

        return productMapper.DomainPageListToResponsePageList(productPageList);
    }

    private async Task<bool> IsValidAsync(Product product)
    {
        var validationResult = await validator.ValidateAsync(product);

        if (validationResult.IsValid)
        {
            return true;
        }

        foreach (var error in validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}