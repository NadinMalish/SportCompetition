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
            return Context.potents.Where(x => !x.deleted).OrderBy(x=>x.dat_reg).ToList();
        }


        public async Task<Potent> GetPotentByEmail(string email)
        {
            Potent item = await Context.potents.FirstOrDefaultAsync(x => !x.deleted && x.email.ToUpper() == email.ToUpper());
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ApplicationException("Record not found");
            }
        }


        public async Task<Potent> GetPotentByLogin(string login, string Hpass)
        {
            Potent item = await Context.potents.FirstOrDefaultAsync(x => !x.deleted && x.login.ToUpper() == login.ToUpper() && x.pass == Hpass);
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
            int cEm = Context.potents.Where(x => !x.deleted && x.email.ToUpper() == request.email.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Email is not Unique");
            int cLg = Context.potents.Where(x => !x.deleted && x.login.ToUpper() == request.login.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Login is not Unique");

            try
            {
                var _entity = Context.potents.Add(request);
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
            int cLg = Context.potents.Where(x => !x.deleted && x.Id != id && x.login.ToUpper() == request.login.ToUpper()).Count();
            if (cLg > 0) throw new ApplicationException("Login is not Unique");

            try
            {
                Context.potents.Entry(request).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }


        public async Task<bool> SetDelPotent(int id)
        {
            Potent item = await Context.potents.FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                item.deleted = true;
                Context.potents.Update(item);
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
