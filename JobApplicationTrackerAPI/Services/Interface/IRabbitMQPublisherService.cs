namespace JobApplicationTrackerAPI.Services.Interface
{
    public interface IRabbitMQPublisherService
    {
        Task PublishAsync<T>(T message, string queueName);

    }
}