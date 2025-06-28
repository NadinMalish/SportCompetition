using Domain.Entities;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Infrastructure.Repositories.Implementations
{
    public class PotentRepository : EFRepository<Potent>
    {
        public PotentRepository(Context context) : base(context) { }

        public async Task<List<Potent>> GetSpisPotent()
        {
            return await Context.Potents.OrderBy(x=>x.DatReg).ToListAsync();
        }


        public async Task<Potent> GetPotentByEmail(string email)
        {
            Potent item = await Context.Potents.FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ApplicationException("Record not found");
            }
        }


        public async Task<Potent> GetPotentByLogin(string login)//, string Hpass)
        {
            // TODO: что-то изменится, когда заработает авторизация. А пока так
            Potent item = await Context.Potents.FirstOrDefaultAsync(x => x.Login.ToUpper() == login.ToUpper()/* && x.Pass == Hpass*/);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ApplicationException("Record not found");
            }
        }


        public async Task<Potent> AddPotent(Potent request)
        {
            int cEm = Context.Potents.Where(x => x.Email.ToUpper() == request.Email.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Email is not Unique");
            int cLg = Context.Potents.Where(x => x.Login.ToUpper() == request.Login.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Login is not Unique");

            try
            {
                var _entity = Context.Potents.Add(request);
                await Context.SaveChangesAsync();
                return _entity.Entity;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }


        public async Task UpdPotent(int id, Potent request)
        {
            int cLg = Context.Potents.Where(x => x.Id != id && x.Login.ToUpper() == request.Login.ToUpper()).Count();
            if (cLg > 0) throw new ApplicationException("Login is not Unique");

            try
            {
                Context.Potents.Entry(request).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }


        public async Task<bool> SetDelPotent(int id)
        {
            Potent item = await Context.Potents.FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                //item.Deleted = true;
                //Context.Potents.Update(item);
                Context.Potents.Remove(item);
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
