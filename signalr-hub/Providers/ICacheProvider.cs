namespace signalr_hub.Providers;

public interface ICacheProvider
{
    Task<string> TryGetValueAsync(string key);

    Task<T> TryGetValueAsync<T>(string key);

    Task<T> TrySetValueAsync<T>(string key, T value);

    Task TrySetValueAsync(string key, string value);

    Task TryDeleteValueAsync(string key);
}