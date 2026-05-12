namespace DevTask.Core.Models.Options.RabbitMq
{
    public class RabbitMqConfig
    {
        //TODO move it to secret storage
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string Username { get; set; } = "admin";
        public string Password { get; set; } = "QWEasd123!";
    }
}
