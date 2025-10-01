namespace RedisService
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken ct = default);
        Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct = default);
        Task RemoveAsync(string key, CancellationToken ct = default);
        Task RemoveByPrefixAsync(string prefix, CancellationToken ct = default);
        TimeSpan DefaultTtl { get; }
        string Key(params object[] parts);
    }

    public sealed class RedisCacheOptions
    {
        public int DefaultTTLSeconds { get; set; } = 60;
    }
}
