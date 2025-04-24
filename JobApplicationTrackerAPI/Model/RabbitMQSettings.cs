namespace JobApplicationTrackerAPI.Model
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; } = "";
        public int Port { get; set; } = 0;
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}