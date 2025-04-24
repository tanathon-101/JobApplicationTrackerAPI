using JobApplicationTrackerAPI.Services.Interface;
using JobApplicationTrackerAPI.Model;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace JobApplicationTrackerAPI.Services;

public class RabbitMQPublisherService : IRabbitMQPublisherService
{
    private readonly RabbitMQSettings _settings;
    private readonly ConnectionFactory _factory;
    private static bool _firstRun = true;

    public RabbitMQPublisherService(IOptions<RabbitMQSettings> options)
    {
        Console.WriteLine("⚠️ RabbitMQPublisherService ctor called");

        _settings = options.Value ?? throw new ArgumentNullException(nameof(options));

        _factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
        };

        Console.WriteLine($"[RabbitMQ] Configured → Host: {_settings.HostName}, Port: {_settings.Port}, User: {_settings.UserName}");
    }

    public async Task PublishAsync<T>(T message, string queueName)
    {
        if (_firstRun)
        {
            _firstRun = false;
            Console.WriteLine("[RabbitMQ] Initial startup delay: waiting 5 seconds...");
            await Task.Delay(5000);
        }

        var endpoints = new List<AmqpTcpEndpoint>
        {
            new AmqpTcpEndpoint(_settings.HostName, _settings.Port)
        };

        using var connection = await CreateConnectionWithRetryAsync(endpoints);
        Console.WriteLine("[RabbitMQ] ✅ Connected to RabbitMQ");

        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var props = new BasicProperties
        {
            ContentType = "application/json"
        };

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        try
        {
            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: queueName,
                mandatory: false,
                basicProperties: props,
                body: body.AsMemory()
            );

            Console.WriteLine($"[RabbitMQ] ✅ Published to queue '{queueName}' → {json}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RabbitMQ] ❌ Failed to publish: {ex.Message}");
            throw;
        }
    }

    private async Task<IConnection> CreateConnectionWithRetryAsync(List<AmqpTcpEndpoint> endpoints, int maxRetry = 10)
    {
        for (int i = 1; i <= maxRetry; i++)
        {
            try
            {
                Console.WriteLine($"[RabbitMQ] Attempt {i}: Connecting to {_settings.HostName}:{_settings.Port}");
                return await _factory.CreateConnectionAsync(endpoints);
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"[RabbitMQ] ❌ Attempt {i} failed: {ex.Message}");

                if (i == maxRetry)
                {
                    Console.WriteLine("[RabbitMQ] ❌ Max retry reached, giving up.");
                    throw;
                }

                var delayMs = (int)Math.Pow(2, i) * 1000;
                Console.WriteLine($"[RabbitMQ] ⏳ Retrying in {delayMs / 1000} seconds...");
                await Task.Delay(delayMs);
            }
        }

        throw new Exception("RabbitMQ connection failed after all retries.");
    }
}
