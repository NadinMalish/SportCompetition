using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Core.Domain;

namespace WebApplication.DataAccess.Repositories
{
    public class RoleRepository : EFRepository<Role>
    {
        public RoleRepository(Context context) : base(context)
        { }
    }
}
