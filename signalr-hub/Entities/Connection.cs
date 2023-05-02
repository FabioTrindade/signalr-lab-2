namespace signalr_hub.Entities;

public record Connection : Entity
{
    public Connection(string? connectionId
        , string? analyst
        , string? group)
    {
        ConnectionId = connectionId;
        Analyst = analyst;
        Group = group;
    }

    public string? ConnectionId { get; private set; }

    public string? Analyst { get; private set; }

    public string? Group { get; private set; }
}