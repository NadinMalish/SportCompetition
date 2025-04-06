using Microsoft.EntityFrameworkCore;
using SportCompetition.Domain.Entities;

namespace SportCompetition.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //    Database.EnsureCreated(); 
        }

        /// <summary>
        /// Состязание мероприятия
        /// </summary>
        public DbSet<Competition> Competitions { get; set; }

        /// <summary>
        /// Мероприятие
        /// </summary>
        public DbSet<EventInfo> Events { get; set; }

        /// <summary>
        /// Команда мероприятия
        /// </summary>
        public DbSet<Team> Teams { get; set; }

    }
}
