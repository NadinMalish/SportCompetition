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




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doc>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("docs_pkey");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_docs'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.file_name).HasMaxLength(400);
                entity.Property(e => e.name_doc).HasMaxLength(2000);

                entity.HasOne(d => d.DocType).WithMany(p => p.Docs)
                    .HasForeignKey(d => d.id_doc_type)
                    .HasConstraintName("fk_docs_doctypes");
            });

            modelBuilder.Entity<DocType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("doc_types_pkey");
                entity.HasIndex(e => e.name_doc_type, "ukdoctypes_namedoctype").IsUnique();
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_doc_types'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.comment_doc).HasMaxLength(4000);
                entity.Property(e => e.name_doc_type).HasMaxLength(2000);
            });

            modelBuilder.Entity<Potent>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("potents_pkey");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("nextval('sq_potents'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.dat_reg)
                    .HasDefaultValueSql("now()")
                    .HasColumnType("timestamp without time zone");
                entity.Property(e => e.email).HasMaxLength(40);
                entity.Property(e => e.firstname).HasMaxLength(20);
                entity.Property(e => e.lastname).HasMaxLength(20);
                entity.Property(e => e.login).HasMaxLength(20);
                entity.Property(e => e.pass).HasMaxLength(64);
                entity.Property(e => e.surname).HasMaxLength(20);
            });

            modelBuilder.HasSequence("sq_doc_types");
            modelBuilder.HasSequence("sq_docs");
            modelBuilder.HasSequence("sq_potents");


            base.OnModelCreating(modelBuilder);
        }
    }
}
