using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using RedisService;
using System.Net.Sockets;

namespace Infrastructure.Repositories.Implementations
{
    public class DocRepository : EFRepository<Doc>
    {
        public DocRepository(Context context, ICacheService? cache = null) : base(context, cache) { }

        public async Task<List<Doc>> GetDocsByEventId(int eventId)
        {
            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "byEvent", eventId);
                var cached = await Cache.GetAsync<List<Doc>>(key);
                if (cached != null) return cached;
            }

            var list = await _data.Where(doc => doc.IdEvent == eventId).ToListAsync();

            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "byEvent", eventId);
                await Cache.SetAsync(key, list, Cache.DefaultTtl);
            }
            return list;
        }

        public async Task<List<Doc>> GetDocsByCompetitionId(int competitionId)
        {
            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "byComp", competitionId);
                var cached = await Cache.GetAsync<List<Doc>>(key);
                if (cached != null) return cached;
            }

            var list = await _data.Where(com => com.IdCompetition == competitionId).ToListAsync();

            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "byComp", competitionId);
                await Cache.SetAsync(key, list, Cache.DefaultTtl);
            }
            return list;
        }
    }
}
