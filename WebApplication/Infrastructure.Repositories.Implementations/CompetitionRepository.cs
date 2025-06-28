using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class CompetitionRepository : EFRepository<Competition>
    {
        public CompetitionRepository(Context context) : base(context)
        {
        }

        public async Task<List<Competition>> GetCompetitionsAsync(bool asNoTracking = false)
        {
            IQueryable<Competition> query = Context.Competitions.Include(ep => ep.Event);
            CheckAsNoTracking(ref query, asNoTracking);
            return await query.ToListAsync();
        }
    }
}