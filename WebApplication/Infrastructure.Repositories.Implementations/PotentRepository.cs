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
<<<<<<< HEAD
            return Context.Potents.Where(x => !x.Deleted).OrderBy(x=>x.DatReg).ToList();
=======
            return Context.potents.Where(x => !x.Deleted).OrderBy(x=>x.DatReg).ToList();
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
        }


        public async Task<Potent> GetPotentByEmail(string email)
        {
<<<<<<< HEAD
            Potent item = await Context.Potents.FirstOrDefaultAsync(x => !x.Deleted && x.Email.ToUpper() == email.ToUpper());
=======
            Potent item = await Context.potents.FirstOrDefaultAsync(x => !x.Deleted && x.Email.ToUpper() == email.ToUpper());
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
            Potent item = await Context.Potents.FirstOrDefaultAsync(x => !x.Deleted && x.Login.ToUpper() == login.ToUpper() && x.Pass == Hpass);
=======
            Potent item = await Context.potents.FirstOrDefaultAsync(x => !x.Deleted && x.Login.ToUpper() == login.ToUpper() && x.Pass == Hpass);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
            int cEm = Context.Potents.Where(x => !x.Deleted && x.Email.ToUpper() == request.Email.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Email is not Unique");
            int cLg = Context.Potents.Where(x => !x.Deleted && x.Login.ToUpper() == request.Login.ToUpper()).Count();
=======
            int cEm = Context.potents.Where(x => !x.Deleted && x.Email.ToUpper() == request.Email.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Email is not Unique");
            int cLg = Context.potents.Where(x => !x.Deleted && x.Login.ToUpper() == request.Login.ToUpper()).Count();
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            if (cEm > 0) throw new ApplicationException("Login is not Unique");

            try
            {
<<<<<<< HEAD
                var _entity = Context.Potents.Add(request);
=======
                var _entity = Context.potents.Add(request);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
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
<<<<<<< HEAD
            int cLg = Context.Potents.Where(x => !x.Deleted && x.Id != id && x.Login.ToUpper() == request.Login.ToUpper()).Count();
=======
            int cLg = Context.potents.Where(x => !x.Deleted && x.Id != id && x.Login.ToUpper() == request.Login.ToUpper()).Count();
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            if (cLg > 0) throw new ApplicationException("Login is not Unique");

            try
            {
<<<<<<< HEAD
                Context.Potents.Entry(request).State = EntityState.Modified;
=======
                Context.potents.Entry(request).State = EntityState.Modified;
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }


        public async Task<bool> SetDelPotent(int id)
        {
<<<<<<< HEAD
            Potent item = await Context.Potents.FirstOrDefaultAsync(x => x.Id == id);
=======
            Potent item = await Context.potents.FirstOrDefaultAsync(x => x.Id == id);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3

            if (item != null)
            {
                item.Deleted = true;
<<<<<<< HEAD
                Context.Potents.Update(item);
=======
                Context.potents.Update(item);
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
