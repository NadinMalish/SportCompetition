using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SportCompetition.Domain;
using SportCompetition.Model;

namespace SportCompetition.Infrastructure
{
    public class PotentRepository : IPotentRepository
    {
        private readonly CompetitionContext db;
        private readonly DbSet<Potent> potentSet;

        public PotentRepository(CompetitionContext context)
        {
            db = context;
            potentSet = db.Set<Potent>();
        }


        public async Task<Potent> GetPotentByEmail(string email)
        {
            Potent item = await potentSet.FirstOrDefaultAsync(x => !x.Deleted && x.email == email);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ApplicationException("Record not found");
            }
        }

        public async Task<Potent> GetPotentByLogin(string login, string pass)
        {
            Potent item = await potentSet.FirstOrDefaultAsync(x => !x.Deleted && x.login.ToUpper() == login.ToUpper() && x.pass==DtPass.HashPass(pass));
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
            int cEm = potentSet.Where(x => !x.Deleted && x.email.ToUpper() == request.email.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Email is not Unique");
            int cLg = potentSet.Where(x => !x.Deleted && x.login.ToUpper() == request.login.ToUpper()).Count();
            if (cEm > 0) throw new ApplicationException("Login is not Unique");

            Potent item = new Potent()
            {
                lastname = request.lastname,
                firstname = request.firstname,
                surname = request.surname,
                date_birth = request.date_birth,
                email = request.email,
                login = request.login,
                pass = DtPass.HashPass(request.pass)
            };

            var _entity = potentSet.Add(item);
            await db.SaveChangesAsync();
            return _entity.Entity;
        }

        public async Task UpdPotent(PotentShortResponse request)
        {
            Potent item = await potentSet.FirstOrDefaultAsync(x => !x.Deleted && x.id == request.id);
            if (item != null)
            {
                item.lastname = request.lastname;
                item.firstname = request.firstname;
                item.surname = request.surname;
                item.date_birth = request.date_birth;
                try
                {
                    db.Entry(item).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"{ex.Message}");
                }
            }
            else 
            { 
                throw new ApplicationException("Record not found"); 
            }
        }
    }
}
