using SportCompetition.Model;

namespace SportCompetition.Infrastructure
{
    public interface IRepositoryExtD<T>  where T : BaseEntityDelExt
    {
        Task<bool> SetDelById(int id);

        Task<IEnumerable<T>> GetAllNotDell();
    }
}
