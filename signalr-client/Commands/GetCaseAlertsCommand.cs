namespace signalr_client.Commands
{
    public record GetCaseAlertsCommand
    {
        public string Analyst { get; set; }

        public string Group { get; set; }
    }
}
