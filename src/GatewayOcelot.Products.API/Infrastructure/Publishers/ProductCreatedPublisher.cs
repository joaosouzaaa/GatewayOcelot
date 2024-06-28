using GatewayOcelot.Products.API.Constants;
using GatewayOcelot.Products.API.Contracts;
using GatewayOcelot.Products.API.Interfaces.Infrastructure.Publishers;
using GatewayOcelot.Products.API.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GatewayOcelot.Products.API.Infrastructure.Publishers;

public sealed class ProductCreatedPublisher(IOptions<RabbitMQOptions> rabbitMQOptions) : IProductCreatedPublisher
{
    private readonly RabbitMQOptions _rabbitMQ = rabbitMQOptions.Value;

    public void PublishProductCreatedEventMessage(ProductCreatedEvent productCreatedEvent)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri(_rabbitMQ.Uri)
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(QueuesConstants.ProductCreatedQueue, false, false, false);

        var productCreatedEventJsonString = JsonSerializer.Serialize(productCreatedEvent);
        var body = Encoding.UTF8.GetBytes(productCreatedEventJsonString);

        channel.BasicPublish(exchange: "", routingKey: QueuesConstants.ProductCreatedQueue, basicProperties: null, body: body);
    }
}
