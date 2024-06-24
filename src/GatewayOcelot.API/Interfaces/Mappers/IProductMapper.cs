using GatewayOcelot.API.DataTransferObjects.Product;
using GatewayOcelot.API.Entities;
using GatewayOcelot.API.Settings.PaginationSettings;

namespace GatewayOcelot.API.Interfaces.Mappers;

public interface IProductMapper
{
    Product CreateToDomain(ProductCreateRequest productCreateRequest);
    Product UpdateToDomain(ProductUpdateRequest productUpdateRequest);
    ProductResponse DomainToResponse(Product product);
    PageList<ProductResponse> DomainPageListToResponsePageList(PageList<Product> productPageList);
}
