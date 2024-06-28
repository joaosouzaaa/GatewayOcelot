using FluentValidation;
using GatewayOcelot.Products.API.Contracts;
using GatewayOcelot.Products.API.DataTransferObjects.Product;
using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Enums;
using GatewayOcelot.Products.API.Extensions;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.Publishers;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.Repositories;
using GatewayOcelot.Products.API.Interfaces.Mappers;
using GatewayOcelot.Products.API.Interfaces.Services;
using GatewayOcelot.Products.API.Interfaces.Settings;
using GatewayOcelot.Products.API.Settings.PaginationSettings;

namespace GatewayOcelot.Products.API.Services;

public sealed class ProductService(
    IProductRepository productRepository,
    IProductMapper productMapper,
    IZipCodeService zipCodeService,
    IProductCreatedPublisher productCreatedPublisher,
    IValidator<Product> validator,
    INotificationHandler notificationHandler)
    : IProductService
{
    public async Task AddAsync(ProductCreateRequest productCreateRequest)
    {
        var product = productMapper.CreateToDomain(productCreateRequest);

        var address = await zipCodeService.GetAddressByRequestAsync(productCreateRequest.Address);

        if (address is null)
        {
            notificationHandler.AddNotification(nameof(ENotificationMessages.NotFound), ENotificationMessages.NotFound.Description().FormatTo("Address"));

            return;
        }

        product.Address = address;

        if (!await IsValidAsync(product))
        {
            return;
        }

        await productRepository.InsertOneAsync(product);

        productCreatedPublisher.PublishProductCreatedEventMessage(new ProductCreatedEvent(product.Id));
    }

    public async Task UpdateAsync(ProductUpdateRequest productUpdateRequest)
    {
        if (!await productRepository.ExistsAsync(productUpdateRequest.Id))
        {
            notificationHandler.AddNotification(nameof(ENotificationMessages.NotFound), ENotificationMessages.NotFound.Description().FormatTo("Product"));

            return;
        }

        var product = productMapper.UpdateToDomain(productUpdateRequest);

        var address = await zipCodeService.GetAddressByRequestAsync(productUpdateRequest.Address);

        if (address is null)
        {
            notificationHandler.AddNotification(nameof(ENotificationMessages.NotFound), ENotificationMessages.NotFound.Description().FormatTo("Address"));

            return;
        }

        product.Address = address;

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
            notificationHandler.AddNotification(nameof(ENotificationMessages.NotFound), ENotificationMessages.NotFound.Description().FormatTo("Product"));

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
