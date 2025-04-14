using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class EventParticipantRepository : EFRepository<EventParticipant>
    {
        public EventParticipantRepository(Context context) : base(context)
        {
        }

        public async Task<List<EventParticipant>> GetParticipantAsync(bool asNoTracking = false)
        {
            IQueryable<EventParticipant> query = Context.EventParticipants.Include(ep => ep.Role)
                                                                          .Include(ep => ep.Status);
            checkAsNoTracking(ref query, asNoTracking);
            return await query.ToListAsync();
        }

        public async Task<EventParticipant?> GetEventParticipantById(Guid id, bool asNoTracking = false) 
        {
            IQueryable<EventParticipant> query = Context.EventParticipants;
            checkAsNoTracking(ref query, asNoTracking);
            return await query.SingleOrDefaultAsync(ep => ep.Id == id);
        }

        public async Task AddEventParticipantAsync(EventParticipant eventParticipant) 
        {
            await AddAsync(eventParticipant);
            await SaveChangesAsync();
        }
    }
}
