
using Microsoft.EntityFrameworkCore;
using SportCompetition.Domain.Entities;

namespace SportCompetition.Infrastructure
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;

        public EfRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entityList = await _dataContext.Set<T>().ToListAsync();
            return entityList;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(IEnumerable<int> ids)
        {
            var entities = await _dataContext.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
            return entities;
        }

        public async Task<int> AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateAsync(T entity)
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

    }
}
