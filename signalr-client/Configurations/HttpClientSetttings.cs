namespace signalr_client.Configurations
{
    public record HttpClientSetttings
    {
        public string? BaseUrl { get; set; }

        public string? GroupAlert { get; set; }

        public string? CaseAlert { get; set; }

        public string? UpdateCaseAlert { get; set; }
    }
}
