using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace signalr_hub.Providers;

public class RedisCacheProvider : ICacheProvider
{
    private readonly ILogger<RedisCacheProvider> _logger;
    private readonly IDistributedCache _redis;

    public RedisCacheProvider(ILogger<RedisCacheProvider> logger
        , IDistributedCache redis)
    {
        _logger = logger;
        _redis = redis;
    }

    public async Task<string> TryGetValueAsync(string key)
    {
        _logger.LogInformation($"Starting {nameof(TryGetValueAsync)} by key: {key}");

        var value = await _redis.GetStringAsync(key);

        return value;
    }

    public async Task<T> TryGetValueAsync<T>(string key)
    {
        _logger.LogInformation($"Starting {nameof(TryGetValueAsync)} by key: {key}");

        var value = await _redis.GetStringAsync(key);

        if (value != null)
            return JsonSerializer.Deserialize<T>(value);

        _logger.LogInformation($"Finish {nameof(TryGetValueAsync)}");

        return default(T);
    }

    public async Task<T> TrySetValueAsync<T>(string key, T value)
    {
        _logger.LogInformation($"Starting {nameof(TrySetValueAsync)} key: {key} <T>: {value}");

        await _redis.SetStringAsync(key, JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            SlidingExpiration = TimeSpan.FromMinutes(60)
        });

        _logger.LogInformation($"Finish {nameof(TrySetValueAsync)} key <T>");

        return value;
    }

    public async Task TrySetValueAsync(string key, string value)
    {
        _logger.LogInformation($"Starting {nameof(TrySetValueAsync)} key: {key} value:{value}");

        await _redis.SetStringAsync(key, value);

        _logger.LogInformation($"Finish {nameof(TrySetValueAsync)}");
    }

    public async Task TryDeleteValueAsync(string key)
    {
        _logger.LogInformation($"Starting {nameof(TryDeleteValueAsync)} key: {key}");

        await _redis.RemoveAsync(key);

        _logger.LogInformation($"Finish {nameof(TryDeleteValueAsync)}");
    }
}