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
<<<<<<< HEAD
            IQueryable<EventParticipant> query = Context.EventParticipants.Include(ep => ep.Status);
            CheckAsNoTracking(ref query, asNoTracking);
=======
            IQueryable<EventParticipant> query = Context.EventParticipants.Include(ep => ep.Role)
                                                                          .Include(ep => ep.Status);
            checkAsNoTracking(ref query, asNoTracking);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            return await query.ToListAsync();
        }

        public async Task<EventParticipant?> GetEventParticipantById(int id, bool asNoTracking = false) 
        {
<<<<<<< HEAD
            IQueryable<EventParticipant> query = Context.EventParticipants.Include(ep => ep.Status);
            CheckAsNoTracking(ref query, asNoTracking);
=======
            IQueryable<EventParticipant> query = Context.EventParticipants.Include(ep => ep.Role)
                                                                          .Include(ep => ep.Status);
            checkAsNoTracking(ref query, asNoTracking);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            return await query.SingleOrDefaultAsync(ep => ep.Id == id);
        }

        public async Task AddEventParticipantAsync(EventParticipant eventParticipant) 
        {
            await AddAsync(eventParticipant);
            await SaveChangesAsync();
        }
    }
}
