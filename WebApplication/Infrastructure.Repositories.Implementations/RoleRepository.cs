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
            return Context.Roles.AsNoTracking().SingleOrDefault(s => s.Name == name) != null;
        }

        public async Task<bool> IsRoleExistsByIdAsync(Guid id)
        {
            return Context.Roles.AsNoTracking().SingleOrDefault(s => s.Id == id) != null;
        }
    }
}
