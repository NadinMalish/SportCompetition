﻿// <auto-generated />
using System;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("sq_competitions");

            modelBuilder.HasSequence("sq_doc_types");

            modelBuilder.HasSequence("sq_docs");

            modelBuilder.HasSequence("sq_events");

            modelBuilder.HasSequence("sq_potents");

            modelBuilder.HasSequence("sq_teams");

            modelBuilder.Entity("Domain.Entities.ApplicationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationStatuses");
                });

            modelBuilder.Entity("Domain.Entities.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('sq_competitions'::regclass)");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CompetitionType")
                        .HasColumnType("integer");

                    b.Property<int>("EditorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int?>("MaxComandSize")
                        .HasColumnType("integer");

                    b.Property<int?>("MinComandSize")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("competition_pkey");

                    b.HasIndex("EditorId");

                    b.HasIndex("EventId");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("Domain.Entities.Doc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('sq_docs'::regclass)");

                    b.Property<string>("comment_doc")
                        .HasColumnType("text");

                    b.Property<bool>("deleted")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("docum")
                        .HasColumnType("bytea");

                    b.Property<string>("file_name")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<int?>("id_doc_type")
                        .HasColumnType("integer");

                    b.Property<int?>("id_event")
                        .HasColumnType("integer");

                    b.Property<int?>("id_event_competition")
                        .HasColumnType("integer");

                    b.Property<string>("name_doc")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.HasKey("Id")
                        .HasName("docs_pkey");

                    b.HasIndex("id_doc_type");

                    b.ToTable("docs");
                });

            modelBuilder.Entity("Domain.Entities.DocType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('sq_doc_types'::regclass)");

                    b.Property<string>("comment_doc")
                        .HasMaxLength(4000)
                        .HasColumnType("character varying(4000)");

                    b.Property<string>("name_doc_type")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.HasKey("Id")
                        .HasName("doc_types_pkey");

                    b.HasIndex(new[] { "name_doc_type" }, "ukdoctypes_namedoctype")
                        .IsUnique();

                    b.ToTable("doc_types");
                });

            modelBuilder.Entity("Domain.Entities.EventInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('sq_events'::regclass)");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Feedback")
                        .HasColumnType("text");

                    b.Property<DateTime?>("FinishActualControlDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FinishRegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("StartActualControlDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartRegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("event_info_pkey");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Domain.Entities.EventParticipant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(4096)
                        .HasColumnType("character varying(4096)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActual")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCaptainConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int?>("SetStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("EventParticipants");
                });

            modelBuilder.Entity("Domain.Entities.Potent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('sq_potents'::regclass)");

                    b.Property<DateTime>("dat_reg")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<DateOnly>("date_birth")
                        .HasColumnType("date");

                    b.Property<bool>("deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("firstname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("gender")
                        .HasColumnType("integer");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("pass")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("surname")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id")
                        .HasName("potents_pkey");

                    b.ToTable("potents");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasDefaultValueSql("nextval('sq_teams'::regclass)");

                    b.Property<int>("CaptainId")
                        .HasColumnType("integer");

                    b.Property<int>("CompetitionOfEventId")
                        .HasColumnType("integer");

                    b.Property<int?>("ConsidererId")
                        .HasColumnType("integer");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RejectNote")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("team_pkey");

                    b.HasIndex("CaptainId");

                    b.HasIndex("CompetitionOfEventId");

                    b.HasIndex("ConsidererId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Domain.Entities.Competition", b =>
                {
                    b.HasOne("Domain.Entities.Potent", "Editor")
                        .WithMany("Competitions")
                        .HasForeignKey("EditorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_competition_potent");

                    b.HasOne("Domain.Entities.EventInfo", "Event")
                        .WithMany("Competitions")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_competition_event_info");

                    b.Navigation("Editor");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Domain.Entities.Doc", b =>
                {
                    b.HasOne("Domain.Entities.DocType", "DocType")
                        .WithMany("Docs")
                        .HasForeignKey("id_doc_type")
                        .HasConstraintName("fk_docs_doctypes");

                    b.Navigation("DocType");
                });

            modelBuilder.Entity("Domain.Entities.EventInfo", b =>
                {
                    b.HasOne("Domain.Entities.Potent", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_entity_info_potent");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("Domain.Entities.EventParticipant", b =>
                {
                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("EventParticipants")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ApplicationStatus", "Status")
                        .WithMany("EventParticipants")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.HasOne("Domain.Entities.Potent", "Captain")
                        .WithMany("CreatedTeams")
                        .HasForeignKey("CaptainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_competition_potent_by_captain");

                    b.HasOne("Domain.Entities.Competition", "CompetitionOfEvent")
                        .WithMany()
                        .HasForeignKey("CompetitionOfEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Potent", "Considerer")
                        .WithMany("ConsideredTeams")
                        .HasForeignKey("ConsidererId")
                        .HasConstraintName("fk_competition_potent_by_considerer");

                    b.Navigation("Captain");

                    b.Navigation("CompetitionOfEvent");

                    b.Navigation("Considerer");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationStatus", b =>
                {
                    b.Navigation("EventParticipants");
                });

            modelBuilder.Entity("Domain.Entities.DocType", b =>
                {
                    b.Navigation("Docs");
                });

            modelBuilder.Entity("Domain.Entities.EventInfo", b =>
                {
                    b.Navigation("Competitions");
                });

            modelBuilder.Entity("Domain.Entities.Potent", b =>
                {
                    b.Navigation("Competitions");

                    b.Navigation("ConsideredTeams");

                    b.Navigation("CreatedTeams");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("EventParticipants");
                });
#pragma warning restore 612, 618
        }
    }
}
