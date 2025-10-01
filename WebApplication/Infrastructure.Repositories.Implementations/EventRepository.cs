using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using RedisService;
using System.Linq.Expressions;
using EventInfo = Domain.Entities.EventInfo;

namespace Infrastructure.Repositories.Implementations
{
    public class EventRepository : EFRepository<EventInfo>
    {
        public EventRepository(Context context, ICacheService? cache = null) : base(context, cache) { }

        public Task<List<EventInfo>> GetAllAsync(int count = 100, int offset = 0, bool asNoTracking = false, Expression<Func<EventInfo, bool>>? filter = null)
        {
            return GetAllAsync<object>(count, offset, asNoTracking, filter, null, false);
        }

        public async Task<List<EventInfo>> GetAllAsync<TKey>(int count = 100, int offset = 0, bool asNoTracking = false,
        Expression<Func<EventInfo, bool>>? filter = null,
        Expression<Func<EventInfo, TKey>>? orderExpression = null,
        bool sortDescending = false)
        {
            if (Cache != null && filter == null && orderExpression == null && offset == 0)
            {
                var key = Cache.Key(CachePrefix, "list", "count", count, "notrack", asNoTracking);
                var cached = await Cache.GetAsync<List<EventInfo>>(key);
                if (cached != null) return cached;

                var q = _data
                    .Skip(offset).Take(count)
                    .AsQueryable();

                if (asNoTracking)
                    q = q.AsNoTracking();

                var result = await q.ToListAsync();

                await Cache.SetAsync(key, result, Cache.DefaultTtl);
                return result;
            }

            var query = _data.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (orderExpression != null)
                query = sortDescending ? query.OrderByDescending(orderExpression) : query.OrderBy(orderExpression);

            query = query.Skip(offset).Take(count);

            return await (asNoTracking ? query.AsNoTracking().ToListAsync() : query.ToListAsync());
        }

        public async Task<EventInfo?> GetEventInfoById(int id)
        {
            if (Cache != null)
            {
                var key = Cache.Key(CachePrefix, "id", id);
                var cached = await Cache.GetAsync<EventInfo>(key);
                if (cached != null) return cached;

                var entity = await _data.Include(x => x.Competitions).Include(x => x.Organizer)
                                        .FirstOrDefaultAsync(x => x.Id == id);
                if (entity != null)
                    await Cache.SetAsync(key, entity, Cache.DefaultTtl);
                return entity;
            }

            return await _data.Include(x => x.Competitions).Include(x => x.Organizer)
                              .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
