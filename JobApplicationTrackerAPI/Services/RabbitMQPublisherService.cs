using JobApplicationTrackerAPI.Services.Interface;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace JobApplicationTrackerAPI.Services;

public class RabbitMQPublisherService : IRabbitMQPublisherService
{
    private readonly ConnectionFactory _factory;

    public RabbitMQPublisherService()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
        };
    }

    public async Task PublishAsync<T>(T message, string queueName)
    {
        var endpoints = new List<AmqpTcpEndpoint> {
            new("localhost")
        };

        using var connection = await _factory.CreateConnectionAsync(endpoints);
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queueName,
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);

        var props = new BasicProperties();
        props.ContentType = "application/json";
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));


        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            mandatory: false,
            basicProperties: props,
            body: body.AsMemory()
        );



        Console.WriteLine($"[x] Sent '{JsonSerializer.Serialize(message)}'");
    }
}
