using SportCompetition.Domain.Entities;

namespace SportCompetition.Infrastructure
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetRangeByIdsAsync(IEnumerable<int> ids);

        Task<int> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

    }
}
