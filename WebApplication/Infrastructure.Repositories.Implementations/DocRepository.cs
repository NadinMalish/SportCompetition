using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Infrastructure.Repositories.Implementations
{
    public class DocRepository : EFRepository<Doc>
    {
        public DocRepository(Context context) : base(context) { }

        public async Task<List<Doc>> GetDocsByEventId(int eventId) 
        {
            return await _data.Where(doc => doc.IdEvent == eventId).ToListAsync();
        }

        public async Task<List<Doc>> GetDocsByCompetitionId(int competitionId) 
        {
            return await _data.Where(com => com.IdCompetition == competitionId).ToListAsync();
        }
    }
}
