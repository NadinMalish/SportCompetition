using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using RedisService;

namespace Infrastructure.Repositories.Implementations
{
    public class CompetitionRepository : EFRepository<Competition>
    {
        public CompetitionRepository(Context context, ICacheService? cache = null) : base(context, cache) { }

        public async Task<List<Competition>> GetCompetitionsAsync(bool asNoTracking = false)
        {
            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "list", "notrack", asNoTracking);
                var cached = await Cache.GetAsync<List<Competition>>(key);
                if (cached != null) return cached;
            }

            IQueryable<Competition> query = Context.Competitions.Include(ep => ep.Event);
            CheckAsNoTracking(ref query, asNoTracking);
            var data = await query.ToListAsync();

            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "list", "notrack", asNoTracking);
                await Cache.SetAsync(key, data, Cache.DefaultTtl);
            }
            return data;
        }
    }
}