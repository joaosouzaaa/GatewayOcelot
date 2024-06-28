using GatewayOcelot.Products.API.Contracts;

namespace GatewayOcelot.Products.API.Interfaces.Infrastructure.Publishers;

public interface IProductCreatedPublisher
{
    void PublishProductCreatedEventMessage(ProductCreatedEvent productCreatedEvent);
}
