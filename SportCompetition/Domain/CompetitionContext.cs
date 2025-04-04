using Microsoft.EntityFrameworkCore;

namespace SportCompetition.Domain
{
    public class CompetitionContext : DbContext
    {
        public CompetitionContext(DbContextOptions<CompetitionContext> options) : base(options)
        {
            //    Database.EnsureCreated(); 
        }

        public DbSet<Potent> potents { get; set; }
        public DbSet<DocType> doc_types { get; set; }
        public DbSet<Doc> docs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doc>(entity =>
            {
                entity.HasKey(e => e.id).HasName("docs_pkey");
                entity.ToTable("docs", "competition");
                entity.Property(e => e.id)
                    .HasDefaultValueSql("nextval('sq_docs'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.comment_doc).HasColumnName("comment_doc");
                entity.Property(e => e.Deleted)
                    .HasDefaultValue(0)
                    .HasColumnName("deleted")
                    .HasColumnType("INTEGER");
                entity.Property(e => e.docum).HasColumnName("docum");
                entity.Property(e => e.file_name)
                    .HasMaxLength(400)
                    .HasColumnName("file_name");
                entity.Property(e => e.id_doc_type).HasColumnName("id_doc_type");
                entity.Property(e => e.id_event).HasColumnName("id_event");
                entity.Property(e => e.id_event_competition).HasColumnName("id_event_competition");
                entity.Property(e => e.name_doc)
                    .HasMaxLength(2000)
                    .HasColumnName("name_doc");

                entity.HasOne(d => d.DocType).WithMany(p => p.Docs)
                    .HasForeignKey(d => d.id_doc_type)
                    .HasConstraintName("fk_docs_doctypes");
            });

            modelBuilder.Entity<DocType>(entity =>
            {
                entity.HasKey(e => e.id).HasName("doc_types_pkey");
                entity.ToTable("doc_types", "competition");
                entity.HasIndex(e => e.name_doc_type, "ukdoctypes_namedoctype").IsUnique();
                entity.Property(e => e.id)
                    .HasDefaultValueSql("nextval('sq_doc_types'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.comment_doc)
                    .HasMaxLength(4000)
                    .HasColumnName("comment_doc");
                entity.Property(e => e.name_doc_type)
                    .HasMaxLength(2000)
                    .HasColumnName("name_doc_type");
            });

            modelBuilder.Entity<Potent>(entity =>
            {
                entity.HasKey(e => e.id).HasName("potents_pkey");
                entity.ToTable("potents", "competition");
                //entity.HasIndex(e => e.email, "uk_potents_email").IsUnique();
                //entity.HasIndex(e => e.login, "uk_potents_login").IsUnique();
                entity.Property(e => e.id)
                    .HasDefaultValueSql("nextval('sq_potents'::regclass)")
                    .HasColumnName("id");
                entity.Property(e => e.dat_reg)
                    .HasDefaultValueSql("now()")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("dat_reg");
                entity.Property(e => e.date_birth).HasColumnName("date_birth");
                entity.Property(e => e.Deleted)
                    .HasDefaultValue(0)
                    .HasColumnName("deleted")
                    .HasColumnType("INTEGER");
                entity.Property(e => e.email)
                    .HasMaxLength(40)
                    .HasColumnName("email");
                entity.Property(e => e.firstname)
                    .HasMaxLength(20)
                    .HasColumnName("firstname");
                //entity.Property(e => e.gender)
                //    .HasColumnType("bit(3)")
                //    .HasColumnName("gender");
                entity.Property(e => e.lastname)
                    .HasMaxLength(20)
                    .HasColumnName("lastname");
                entity.Property(e => e.login)
                    .HasMaxLength(20)
                    .HasColumnName("login");
                entity.Property(e => e.pass)
                    .HasMaxLength(64)
                    .HasColumnName("pass");
                entity.Property(e => e.surname)
                    .HasMaxLength(20)
                    .HasColumnName("surname");
            });

            modelBuilder.HasSequence("sq_doc_types", "competition");
            modelBuilder.HasSequence("sq_docs", "competition");
            modelBuilder.HasSequence("sq_potents", "competition");

        }

    }
}
