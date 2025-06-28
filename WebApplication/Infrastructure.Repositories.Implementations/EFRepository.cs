using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly Context Context;
        private readonly DbSet<T> _data;

        public EFRepository(Context context)
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

<<<<<<< HEAD
        protected void CheckAsNoTracking(ref IQueryable<T> query, bool asNoTracking)
=======
        protected void checkAsNoTracking(ref IQueryable<T> query, bool asNoTracking)
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        {
            if (asNoTracking)
                query = query.AsNoTracking();
        }

<<<<<<< HEAD
        public async Task<bool> CheckExistsById(int? id)
=======
        public async Task<bool> FlById(int? id)
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        {
            T item = null;
            if (id != null) item = await _data.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return (item != null);
        }

    }
}
