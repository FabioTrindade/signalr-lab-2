namespace signalr_client.Commands;

public record GenericCommandResult<T>
{
    public GenericCommandResult(T data)
    {
        Data = data;
    }

    public T Data { get; set; }
}