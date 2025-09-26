using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EventInfo = Domain.Entities.EventInfo;

namespace Infrastructure.Repositories.Implementations
{
    public class EventRepository : EFRepository<EventInfo>
    {
        public EventRepository(Context context) : base(context) {}

        // Обычный, упрощённый метод (без сортировки)
        public Task<List<EventInfo>> GetAllAsync(int count = 100, int offset = 0, bool asNoTracking = false, Expression<Func<EventInfo, bool>>? filter = null)
        {
            return GetAllAsync<object>(count, offset, asNoTracking, filter, null, false);
        }

        // Расширенный метод с сортировкой
        public Task<List<EventInfo>> GetAllAsync<TKey>(int count = 100, int offset = 0, bool asNoTracking = false, Expression<Func<EventInfo, bool>>? filter = null,
            Expression<Func<EventInfo, TKey>>? orderExpression = null,
            bool sortDescending = false)
        {
            var query = _data.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            if (orderExpression != null)
                query = sortDescending
                    ? query.OrderByDescending(orderExpression)
                    : query.OrderBy(orderExpression);

            query = query.Skip(offset).Take(count);

            return asNoTracking
                ? query.AsNoTracking().ToListAsync()
                : query.ToListAsync();
        }

        public Task<EventInfo?> GetEventInfoById(int id) 
        {
            return _data.Include(x => x.Competitions).Include(x => x.Organizer).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
