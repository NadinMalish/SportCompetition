using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class RoleRepository : EFRepository<Role>
    {
        public RoleRepository(Context context) : base(context)
        { }

        public async Task<bool> IsRoleExistsByNameAsync(string name)
        {
            return await Context.Roles.AsNoTracking().SingleOrDefaultAsync(s => s.Name == name) != null;
        }

        public async Task<bool> IsRoleExistsByIdAsync(int id)
        {
            return await Context.Roles.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id) != null;
        }
    }
}
