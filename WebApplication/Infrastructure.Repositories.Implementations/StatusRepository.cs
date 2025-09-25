using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.DataAccess.Repositories
{
    public class StatusRepository : EFRepository<ApplicationStatus>
    {
        public StatusRepository(Context context) : base(context)
        {
        }

        public async Task<bool> IsStatusExistsByNameAsync(string name) 
        {
            return await Context.ApplicationStatuses.AsNoTracking().SingleOrDefaultAsync(s => s.Name == name) != null;
        }

        public async Task<bool> IsStatusExistsByIdAsync(int id)
        {
            return await Context.ApplicationStatuses.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id) != null;
        }
    }
}
