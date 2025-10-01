using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using RedisService;

namespace Infrastructure.Repositories.Implementations
{
    public class EventParticipantRepository : EFRepository<EventParticipant>
    {
        public EventParticipantRepository(Context context, ICacheService? cache = null) : base(context, cache) { }

        public async Task<List<EventParticipant>> GetParticipantAsync(bool asNoTracking = false)
        {
            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "list", "notrack", asNoTracking);
                var cached = await Cache.GetAsync<List<EventParticipant>>(key);
                if (cached != null) return cached;
            }

            IQueryable<EventParticipant> query = Context.EventParticipants.Include(ep => ep.Status);
            CheckAsNoTracking(ref query, asNoTracking);
            var data = await query.ToListAsync();

            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "list", "notrack", asNoTracking);
                await Cache.SetAsync(key, data, Cache.DefaultTtl);
            }
            return data;
        }

        public async Task<EventParticipant?> GetEventParticipantById(int id, bool asNoTracking = false)
        {
            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "id", id, "notrack", asNoTracking);
                var cached = await Cache.GetAsync<EventParticipant>(key);
                if (cached != null) return cached;
            }

            IQueryable<EventParticipant> query = Context.EventParticipants.Include(ep => ep.Status);
            CheckAsNoTracking(ref query, asNoTracking);
            var item = await query.SingleOrDefaultAsync(ep => ep.Id == id);

            if (item != null && Cache != null)
            {
                var key = Cache.Key(CachePrefix, "id", id, "notrack", asNoTracking);
                await Cache.SetAsync(key, item, Cache.DefaultTtl);
            }
            return item;
        }

        public async Task AddEventParticipantAsync(EventParticipant eventParticipant)
        {
            await AddAsync(eventParticipant);
            await SaveChangesAsync();

            if (Cache != null)
                await Cache.RemoveByPrefixAsync(CachePrefix);
        }
    }
}
