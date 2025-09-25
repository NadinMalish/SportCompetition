using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Infrastructure.Repositories.Implementations
{
    public class DocRepository : EFRepository<Doc>
    {
        public DocRepository(Context context) : base(context) { }

        public async Task<List<Doc>> GetDocList()
        {
            return await Context.Docs.ToListAsync();
        }

        public async Task<Doc> AddDoc(Doc request)
        {
            //Doc item = new Doc()
            //{
            //    name_doc = request.name_doc,
            //    file_name = request.file_name,
            //    comment_doc = request.comment_doc,
            //    id_doc_type = request.id_doc_type,
            //    id_event = request.id_event,
            //    id_competition = request.id_competition,
            //    docum = null
            //};

            try
            {
                var _entity = Context.Add(request);
                await Context.SaveChangesAsync();
                return _entity.Entity;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }

        public async Task<bool> SetDelDoclById(int id)
        {
            Doc item = await Context.Docs.FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                //item.Deleted = true;
                //Context.Docs.Update(item);
                Context.Docs.Remove(item);
                await Context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
