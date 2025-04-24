using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

        public DbSet<Potent> potents { get; set; }
        public DbSet<DocType> doc_types { get; set; }
        public DbSet<Doc> docs { get; set; }

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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doc>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("docs_pkey");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_docs'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.FileName)
                    .HasMaxLength(400)
                    .HasColumnName("file_name");
                entity.Property(e => e.NameDoc)
                    .HasMaxLength(2000)
                    .HasColumnName("name_doc");
                entity.Property(e => e.Docum).HasColumnName("docum");
                entity.Property(e => e.CommentDoc).HasColumnName("comment_doc");
                entity.Property(e => e.Deleted).HasColumnName("deleted");
                entity.Property(e => e.IdCompetition).HasColumnName("id_competition");
                entity.Property(e => e.IdDocType).HasColumnName("id_doc_type");
                entity.Property(e => e.IdEvent).HasColumnName("id_event");


                entity.HasOne(d => d.DocType).WithMany(p => p.Docs)
                    .HasForeignKey(d => d.IdDocType)
                    .HasConstraintName("fk_docs_doctypes");
                entity.HasOne(d => d.EventInfo).WithMany(p => p.Docs)
                    .HasForeignKey(d => d.IdEvent)
                    .HasConstraintName("fk_docs_events");
                entity.HasOne(d => d.Competition).WithMany(p => p.Docs)
                    .HasForeignKey(d => d.IdCompetition)
                    .HasConstraintName("fk_docs_competitions");
            });

            modelBuilder.Entity<DocType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("doc_types_pkey");
                entity.HasIndex(e => e.NameDocType, "ukdoctypes_namedoctype").IsUnique();
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_doc_types'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.CommentDoc)
                    .HasMaxLength(4000)
                    .HasColumnName("comment_doc");
                entity.Property(e => e.NameDocType)
                    .HasMaxLength(2000)
                    .HasColumnName("name_doc_type");
            });

            modelBuilder.Entity<Potent>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("potents_pkey");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_potents'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.DatReg)
                    .HasDefaultValueSql("now()")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dat_reg");
                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .HasColumnName("email");
                entity.Property(e => e.Firstname)
                    .HasMaxLength(20)
                    .HasColumnName("firstname");
                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .HasColumnName("lastname");
                entity.Property(e => e.Login)
                    .HasMaxLength(20)
                    .HasColumnName("login");
                entity.Property(e => e.Pass)
                    .HasMaxLength(64)
                    .HasColumnName("pass");
                entity.Property(e => e.Surname)
                    .HasMaxLength(20)
                    .HasColumnName("surname");
                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .HasColumnName("gender");
                entity.Property(e => e.DateBirth).HasColumnName("date_birth");
                entity.Property(e => e.Deleted).HasColumnName("deleted");
            });

            modelBuilder.Entity<EventInfo>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("event_info_pkey");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_events'::regclass)")
                    .HasColumnName("id");

                entity.HasOne(e => e.Organizer).WithMany(p => p.Events)
                    .HasForeignKey(e => e.OrganizerId)
                    .HasConstraintName("fk_entity_info_potent");
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("competition_pkey");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_competitions'::regclass)")
                    .HasColumnName("id");

                entity.HasOne(c => c.Editor).WithMany(p => p.Competitions)
                    .HasForeignKey(c => c.EditorId)
                    .HasConstraintName("fk_competition_potent");
                entity.HasOne(c => c.Event).WithMany(e => e.Competitions)
                    .HasForeignKey(c => c.EventId)
                    .HasConstraintName("fk_competition_event_info");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("team_pkey");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_teams'::regclass)")
                    .HasColumnName("id");

                entity.HasOne(t => t.Captain).WithMany(p => p.CreatedTeams)
                    .HasForeignKey(t => t.CaptainId)
                    .HasConstraintName("fk_competition_potent_by_captain");
                entity.HasOne(t => t.Considerer).WithMany(p => p.ConsideredTeams)
                    .HasForeignKey(t => t.ConsidererId)
                    .HasConstraintName("fk_competition_potent_by_considerer");
            });

            modelBuilder.HasSequence("sq_doc_types");
            modelBuilder.HasSequence("sq_docs");
            modelBuilder.HasSequence("sq_potents");
            modelBuilder.HasSequence("sq_events");
            modelBuilder.HasSequence("sq_competitions");
            modelBuilder.HasSequence("sq_teams");

            base.OnModelCreating(modelBuilder);
        }
    }
}
