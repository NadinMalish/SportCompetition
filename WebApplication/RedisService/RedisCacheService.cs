using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisService
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _mux;
        private readonly IDatabase _db;
        private readonly JsonSerializerOptions _json;
        public TimeSpan DefaultTtl { get; }

        public RedisCacheService(IConnectionMultiplexer mux, IOptions<RedisCacheOptions> opt)
        {
            _mux = mux;
            _db = mux.GetDatabase();
            DefaultTtl = TimeSpan.FromSeconds(Math.Max(1, opt.Value.DefaultTTLSeconds));
            _json = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                WriteIndented = false
            };
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
        {
            var val = await _db.StringGetAsync(key);
            if (val.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(val!, _json);
        }

        public string Key(params object[] parts) => string.Join(':', parts.Select(p => p?.ToString() ?? "_"));

        public Task RemoveAsync(string key, CancellationToken ct = default) => _db.KeyDeleteAsync(key);

        public async Task RemoveByPrefixAsync(string prefix, CancellationToken ct = default)
        {
            foreach (var endpoint in _mux.GetEndPoints())
            {
                var server = _mux.GetServer(endpoint);
                if (!server.IsConnected) continue;

                var batch = new List<RedisKey>(512);
                var pattern = prefix + "*";
                await foreach (var key in server.KeysAsync(pattern: pattern))
                {
                    batch.Add(key);
                    if (batch.Count >= 512)
                    {
                        await _db.KeyDeleteAsync(batch.ToArray());
                        batch.Clear();
                    }
                }
                if (batch.Count > 0)
                    await _db.KeyDeleteAsync(batch.ToArray());
            }
        }

        public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(value, _json);
            return _db.StringSetAsync(key, json, ttl);
        }
    }
}
