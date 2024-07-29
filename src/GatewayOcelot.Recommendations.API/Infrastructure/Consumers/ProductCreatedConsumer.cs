using GatewayOcelot.Recommendations.API.Constants;
using GatewayOcelot.Recommendations.API.Contracts;
using GatewayOcelot.Recommendations.API.Entities;
using GatewayOcelot.Recommendations.API.Interfaces.Repositories;
using GatewayOcelot.Recommendations.API.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GatewayOcelot.Recommendations.API.Infrastructure.Consumers;

public sealed class ProductCreatedConsumer(
    IOptions<RabbitMQOptions> rabbitMQOptions,
    IServiceScopeFactory scopeFactory)
    : BackgroundService
{
    private readonly RabbitMQOptions _rabbitMQ = rabbitMQOptions.Value;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri(_rabbitMQ.Uri)
        };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: QueuesConstants.ProductCreatedQueue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            using var scope = scopeFactory.CreateScope();

            var recommendationRepository = scope.ServiceProvider.GetRequiredService<IRecommendationRepository>();

            var productCreatedEventJsonString = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var productCreatedEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(productCreatedEventJsonString)!;

            var recommendation = new Recommendation()
            {
                Id = Guid.NewGuid(),
                ProductId = productCreatedEvent.ProductId
            };
            await recommendationRepository.AddAsync(recommendation);

            channel.BasicAck(
                deliveryTag: eventArgs.DeliveryTag,
                multiple: false);
        };

        channel.BasicQos(
            prefetchSize: 0,
            prefetchCount: 1,
            global: false);

        channel.BasicConsume(
            queue: QueuesConstants.ProductCreatedQueue,
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }
}
