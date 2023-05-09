namespace signalr_hub.Configurations
{
    public class DatabaseSettings
    {
        public string? Host { get; set; }

        public string? Port { get; set; }

        public string? Database { get; set; }

        public string? UserId { get; set; }

        public string? Password { get; set; }

        public string ConnectionString => $"Data Source={Host},{Port};Initial Catalog={Database};Persist Security Info=True;User ID={UserId};Password={Password};Pooling=false;ApplicationIntent=ReadWrite;MultiSubnetFailover=True;MultipleActiveResultSets=True;Encrypt=False;";
    }
}
