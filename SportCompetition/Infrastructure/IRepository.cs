using SportCompetition.Domain;

namespace SportCompetition.Infrastructure
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> DelByIdAsync(int id);
        Task<T> AddAsync(T item);
        Task UpdateAsync(T item);
    }
}
