using SportCompetition.Domain;
using SportCompetition.Model;

namespace SportCompetition.Infrastructure
{
    public interface IPotentRepository
    {
        Task<Potent> GetPotentByEmail(string email);
        Task<Potent> GetPotentByLogin(string login, string pass);
        Task<Potent> AddPotent(Potent item);
        Task UpdPotent(PotentShortResponse item);

    }
}
