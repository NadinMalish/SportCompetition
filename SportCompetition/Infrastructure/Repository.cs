using Microsoft.EntityFrameworkCore;
using SportCompetition.Domain;
using System;


namespace SportCompetition.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly CompetitionContext _db;
        private readonly DbSet<T> _entitySet;

        public Repository(CompetitionContext db)
        {
            _db = db;
            _entitySet = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _entitySet;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T item = await _entitySet.FirstOrDefaultAsync(x => x.id == id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ApplicationException("Record not found");
            }
        }

        public async Task<bool> DelByIdAsync(int id)
        {
            T item = await _entitySet.FirstOrDefaultAsync(x => x.id == id);

            if (item != null)
            {
                _db.Entry(item).State = EntityState.Deleted;
                //_entitySet.Remove(item);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<T> AddAsync(T item)
        {
            try
            {
                var _entity = _entitySet.Add(item);
                await _db.SaveChangesAsync();
                return _entity.Entity;
            }
            catch(Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }

        public async Task UpdateAsync(T item)
        {
            try
            {
                _db.Entry(item).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }

    }
}
