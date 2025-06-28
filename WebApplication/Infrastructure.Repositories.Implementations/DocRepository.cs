using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace Infrastructure.Repositories.Implementations
{
    public class DocRepository : EFRepository<Doc>
    {
        public DocRepository(Context context) : base(context) { }

        public async Task<List<Doc>> GetSpisDoc()
        {
<<<<<<< HEAD
            return Context.Docs.Where(x => !x.Deleted).ToList();
=======
            return Context.docs.Where(x => !x.Deleted).ToList();
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
            Doc item = await Context.Docs.FirstOrDefaultAsync(x => x.Id == id);
=======
            Doc item = await Context.docs.FirstOrDefaultAsync(x => x.Id == id);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3

            if (item != null)
            {
                item.Deleted = true;
<<<<<<< HEAD
                Context.Docs.Update(item);
=======
                Context.docs.Update(item);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
