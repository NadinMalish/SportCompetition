using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Core.Domain;

namespace WebApplication.DataAccess.Repositories
{
    public class StatusRepository : EFRepository<ApplicationStatus>
    {
        public StatusRepository(Context context) : base(context)
        {
        }
    }
}
