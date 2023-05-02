namespace signalr_hub.Configurations
{
    public class RedisSettings
    {
        public string? Host { get; set; }

        public string? Port { get; set; }

        public string ConnectionString => $"{Host}:{Port}";
    }
}
