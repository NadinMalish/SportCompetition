using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories.Implementations
{
    public class DocTypeRepository : EFRepository<DocType>
    {
        public DocTypeRepository(Context context) : base(context) { }
    }
}
