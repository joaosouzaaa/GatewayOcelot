using FluentValidation;
using FluentValidation.Results;
using GatewayOcelot.API.DataTransferObjects.Product;
using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Interfaces.Data.Repositories;
using GatewayOcelot.API.Interfaces.Mappers;
using GatewayOcelot.API.Interfaces.Settings;
using GatewayOcelot.API.Services;
using GatewayOcelot.API.Settings.PaginationSettings;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IProductMapper> _productMapperMock;
    private readonly Mock<IValidator<Product>> _validatorMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productMapperMock = new Mock<IProductMapper>();
        _validatorMock = new Mock<IValidator<Product>>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _productService = new ProductService(
            _productRepositoryMock.Object,
            _productMapperMock.Object,
            _validatorMock.Object,
            _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_CallsInsertOne()
    {
        // A
        var productCreateRequest = ProductBuilder.NewObject().CreateRequestBuild();

        var product = ProductBuilder.NewObject().DomainBuild();
        _productMapperMock.Setup(p => p.CreateToDomain(It.IsAny<ProductCreateRequest>()))
            .Returns(product);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _productRepositoryMock.Setup(p => p.InsertOneAsync(It.IsAny<Product>()));

        // A
        await _productService.AddAsync(productCreateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _productRepositoryMock.Verify(p => p.InsertOneAsync(It.IsAny<Product>()), Times.Once());
    }

    [Fact]
    public async Task AddAsync_InvalidEntity_CallsAddNotification()
    {
        // A
        var productCreateRequest = ProductBuilder.NewObject().CreateRequestBuild();

        var product = ProductBuilder.NewObject().DomainBuild();
        _productMapperMock.Setup(p => p.CreateToDomain(It.IsAny<ProductCreateRequest>()))
            .Returns(product);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "other"),
            new("test", "other")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _notificationHandlerMock.Setup(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()));

        // A
        await _productService.AddAsync(productCreateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _productRepositoryMock.Verify(p => p.InsertOneAsync(It.IsAny<Product>()), Times.Never());
    }

    [Fact]
    public async Task UpdateAsync_SuccessfulScenario_CallsReplaceOne()
    {
        // A
        var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();

        _productRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var product = ProductBuilder.NewObject().DomainBuild();
        _productMapperMock.Setup(p => p.UpdateToDomain(It.IsAny<ProductUpdateRequest>()))
            .Returns(product);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _productRepositoryMock.Setup(p => p.ReplaceOneAsync(It.IsAny<Product>()));

        // A
        await _productService.UpdateAsync(productUpdateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _productMapperMock.Verify(p => p.UpdateToDomain(It.IsAny<ProductUpdateRequest>()), Times.Once());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once());
        _productRepositoryMock.Verify(p => p.ReplaceOneAsync(It.IsAny<Product>()), Times.Once());
    }

    [Fact]
    public async Task UpdateAsync_EntityDoesNotExist_CallsAddNotification()
    {
        // A
        var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();

        _productRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _notificationHandlerMock.Setup(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()));

        // A
        await _productService.UpdateAsync(productUpdateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _productMapperMock.Verify(p => p.UpdateToDomain(It.IsAny<ProductUpdateRequest>()), Times.Never());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never());
        _productRepositoryMock.Verify(p => p.ReplaceOneAsync(It.IsAny<Product>()), Times.Never());
    }

    [Fact]
    public async Task UpdateAsync_EntityInvalid_CallsAddNotification()
    {
        // A
        var productUpdateRequest = ProductBuilder.NewObject().UpdateRequestBuild();

        _productRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var product = ProductBuilder.NewObject().DomainBuild();
        _productMapperMock.Setup(p => p.UpdateToDomain(It.IsAny<ProductUpdateRequest>()))
            .Returns(product);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("test", "other"),
            new("test", "other")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _notificationHandlerMock.Setup(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()));

        // A
        await _productService.UpdateAsync(productUpdateRequest);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _productMapperMock.Verify(p => p.UpdateToDomain(It.IsAny<ProductUpdateRequest>()), Times.Once());
        _validatorMock.Verify(v => v.ValidateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once());
        _productRepositoryMock.Verify(p => p.ReplaceOneAsync(It.IsAny<Product>()), Times.Never());
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsTrue()
    {
        // A
        var id = Guid.NewGuid();

        _productRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);

        _productRepositoryMock.Setup(p => p.DeleteOneAsync(It.IsAny<Guid>()));

        // A
        await _productService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _productRepositoryMock.Verify(p => p.DeleteOneAsync(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsFalse()
    {
        // A
        var id = Guid.NewGuid();

        _productRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        _notificationHandlerMock.Setup(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()));

        // A
        await _productService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _productRepositoryMock.Verify(p => p.DeleteOneAsync(It.IsAny<Guid>()), Times.Never());
    }

    [Fact]
    public async Task GetByIdAsync_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var id = Guid.NewGuid();

        var product = ProductBuilder.NewObject().DomainBuild();
        _productRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(product);

        var productResponse = ProductBuilder.NewObject().ResponseBuild();
        _productMapperMock.Setup(p => p.DomainToResponse(It.IsAny<Product>()))
            .Returns(productResponse);

        // A
        var productResponseResult = await _productService.GetByIdAsync(id);

        // A
        _productMapperMock.Verify(p => p.DomainToResponse(It.IsAny<Product>()), Times.Once());

        Assert.NotNull(productResponseResult);
    }

    [Fact]
    public async Task GetByIdAsync_EntityDoesNotExist_ReturnsNull()
    {
        // A
        var id = Guid.NewGuid();

        _productRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
            .Returns(Task.FromResult<Product?>(null));

        // A
        var productResponseResult = await _productService.GetByIdAsync(id);

        // A
        _productMapperMock.Verify(p => p.DomainToResponse(It.IsAny<Product>()), Times.Never());

        Assert.Null(productResponseResult);
    }

    [Fact]
    public async Task GetAllPaginatedAsync_SuccessfulScenario_ReturnsResponsePageList()
    {
        // A
        var pageParameters = new PageParameters()
        {
            PageNumber = 123,
            PageSize = 21313
        };

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
        _productRepositoryMock.Setup(p => p.GetAllPaginatedAsync(It.IsAny<PageParameters>()))
            .ReturnsAsync(productPageList);

        var productResponsePageList = new PageList<ProductResponse>()
        {
            CurrentPage = 123,
            Data =
            [
                ProductBuilder.NewObject().ResponseBuild(),
                ProductBuilder.NewObject().ResponseBuild()
            ],
            PageSize = 13,
            TotalCount = 3945,
            TotalPages = 2
        };
        _productMapperMock.Setup(p => p.DomainPageListToResponsePageList(It.Is<PageList<Product>>(p => p.Data.Count == productPageList.Data.Count)))
            .Returns(productResponsePageList);

        // A
        var productResponsePageListResult = await _productService.GetAllPaginatedAsync(pageParameters);

        // A
        Assert.Equal(productResponsePageListResult.Data.Count, productResponsePageList.Data.Count);
    }
}
