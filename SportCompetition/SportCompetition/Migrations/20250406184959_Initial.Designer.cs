﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SportCompetition.Infrastructure;

#nullable disable

namespace SportCompetition.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250406184959_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SportCompetition.Domain.Entities.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CompetitionType")
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

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("SportCompetition.Domain.Entities.EventInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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

                    b.Property<DateTime>("RegistryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("StartActualControlDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartRegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("SportCompetition.Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CompetitionOfEventId")
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

                    b.HasKey("Id");

                    b.HasIndex("CompetitionOfEventId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("SportCompetition.Domain.Entities.Competition", b =>
                {
                    b.HasOne("SportCompetition.Domain.Entities.EventInfo", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("SportCompetition.Domain.Entities.Team", b =>
                {
                    b.HasOne("SportCompetition.Domain.Entities.Competition", "CompetitionOfEvent")
                        .WithMany()
                        .HasForeignKey("CompetitionOfEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompetitionOfEvent");
                });
#pragma warning restore 612, 618
        }
    }
}
