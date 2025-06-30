using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
        }

        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<Potent> Potents { get; set; }
        public DbSet<DocType> DocTypes { get; set; }
        public DbSet<Doc> Docs { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<EventInfo> Events { get; set; }


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
                entity.Property(e => e.Surname)
                    .HasMaxLength(20)
                    .HasColumnName("surname");
                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .HasColumnName("gender");
                entity.Property(e => e.DateBirth).HasColumnName("date_birth");
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
                entity.HasOne(c => c.Event).WithMany(e => e.Competitions)
                    .HasForeignKey(c => c.EventId)
                    .HasConstraintName("fk_competition_event_info");
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
