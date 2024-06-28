using GatewayOcelot.Products.API.DataTransferObjects.Product;
using GatewayOcelot.Products.API.Entities;
using GatewayOcelot.Products.API.Settings.PaginationSettings;

namespace GatewayOcelot.Products.API.Interfaces.Mappers;

public interface IProductMapper
{
    Product CreateToDomain(ProductCreateRequest productCreateRequest);
    Product UpdateToDomain(ProductUpdateRequest productUpdateRequest);
    ProductResponse DomainToResponse(Product product);
    PageList<ProductResponse> DomainPageListToResponsePageList(PageList<Product> productPageList);
}
