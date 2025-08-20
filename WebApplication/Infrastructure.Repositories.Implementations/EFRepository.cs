using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Implementations
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly Context Context;
        public readonly DbSet<T> _data;

        public EFRepository(Context context)
        {
            Context = context;
            _data = Context.Set<T>();
        }


        // Обычный, упрощённый метод (без сортировки)
        public Task<List<T>> GetAllAsync(int count = 100, int offset = 0, bool asNoTracking = false)
        {
            // Нормализуем параметры
            if (count < 0) count = 0;
            if (offset < 0) offset = 0;

            IQueryable<T> query = _data;

            if (asNoTracking)
                query = query.AsNoTracking();

            if (offset > 0)
                query = query.Skip(offset);

            if (count > 0)
                query = query.Take(count);

            return query.ToListAsync();
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
                Context.Entry(entity).State = EntityState.Deleted;
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

        protected void CheckAsNoTracking(ref IQueryable<T> query, bool asNoTracking)
        {
            if (asNoTracking)
                query = query.AsNoTracking();
        }

        public async Task<bool> CheckExistsById(int? id)
        {
            T item = null;
            if (id != null) item = await _data.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return (item != null);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null) 
        {
            if (filter != null)
                return await _data.Where(filter).CountAsync();
            else
                return await _data.CountAsync();
        }
    }   
}
