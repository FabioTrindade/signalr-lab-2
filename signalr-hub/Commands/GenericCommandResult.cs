namespace signalr_hub.Commands;

public record GenericCommandResult
{
    public GenericCommandResult(object? data)
    {
        Data = data;
    }

    public object? Data { get; set; }
}