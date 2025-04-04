using Microsoft.EntityFrameworkCore;
using SportCompetition.Domain;
using SportCompetition.Model;

namespace SportCompetition.Infrastructure
{
    public class RepositoryExtD<T> : IRepositoryExtD<T> where T : BaseEntityDelExt
    {
        private readonly CompetitionContext _db;
        private readonly DbSet<T> _entitySet;

        public RepositoryExtD(CompetitionContext db)
        {
            _db = db;
            _entitySet = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllNotDell()
        {
            return _entitySet.Where(x => !x.Deleted);
        }

        public async Task<bool> SetDelById(int id)
        {
            T item = await _entitySet.FirstOrDefaultAsync(x => x.id == id);

            if (item != null)
            {
                item.Deleted = true;
                _entitySet.Update(item);
                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
