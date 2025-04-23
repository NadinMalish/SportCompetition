using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public abstract class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly Context Context;
        private readonly DbSet<T> _data;

        protected EFRepository(Context context)
        {
            Context = context;
            _data = Context.Set<T>();

        }

        public Task<List<T>> GetAllAsync(bool asNoTracking = false)
        {
            return asNoTracking ? _data.AsNoTracking().ToListAsync() : _data.ToListAsync();
        }

        public Task<T> GetByIdAsync(int id)
        {
            return _data.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T? entity = await _data.FindAsync(id);
            bool result = false;
            if (entity != null)
            {
                _data.Remove(entity);
                await SaveChangesAsync();
                result = true;
            }
            
            return result;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            bool result = false;
            if (entity != null)
            {
                Context.Entry(entity).State = EntityState.Modified;
                result = true;
                await SaveChangesAsync();
            }
            return result;
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            var ent = (await _data.AddAsync(entity)).Entity;
            await SaveChangesAsync();
            return ent;
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        protected void checkAsNoTracking(ref IQueryable<T> query, bool asNoTracking)
        {
            if (asNoTracking)
                query = query.AsNoTracking();
        }
    }
}
