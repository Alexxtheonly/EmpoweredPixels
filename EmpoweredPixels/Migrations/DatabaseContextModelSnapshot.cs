﻿// <auto-generated />
using System;
using EmpoweredPixels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmpoweredPixels.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmpoweredPixels.Models.Identity.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Issued");

                    b.Property<string>("RefreshValue");

                    b.Property<long>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Identity.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset?>("Banned");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Email");

                    b.Property<bool>("IsVerified");

                    b.Property<DateTimeOffset>("LastLogin");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Identity.Verification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Verifications");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Options");

                    b.Property<DateTimeOffset?>("Started");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchRegistration", b =>
                {
                    b.Property<Guid>("MatchId");

                    b.Property<Guid>("FighterId");

                    b.Property<DateTimeOffset>("Date");

                    b.HasKey("MatchId", "FighterId");

                    b.HasIndex("FighterId");

                    b.ToTable("MatchRegistrations");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MatchId");

                    b.Property<string>("ResultJson");

                    b.HasKey("Id");

                    b.HasIndex("MatchId")
                        .IsUnique();

                    b.ToTable("MatchResults");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchScoreFighter", b =>
                {
                    b.Property<Guid>("MatchId");

                    b.Property<Guid>("FighterId");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<int>("MaxEnergy");

                    b.Property<int>("MaxHealth");

                    b.Property<int>("TotalDamageDone");

                    b.Property<int>("TotalDamageTaken");

                    b.Property<int>("TotalDeaths");

                    b.Property<float>("TotalDistanceTraveled");

                    b.Property<int>("TotalEnergyUsed");

                    b.Property<int>("TotalKills");

                    b.Property<int>("TotalRegeneratedEnergy");

                    b.Property<int>("TotalRegeneratedHealth");

                    b.HasKey("MatchId", "FighterId");

                    b.HasIndex("FighterId");

                    b.ToTable("MatchScoreFighters");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Roster.Fighter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Accuracy");

                    b.Property<float>("Agility");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<float>("Expertise");

                    b.Property<string>("Name");

                    b.Property<float>("Power");

                    b.Property<float>("Regeneration");

                    b.Property<float>("Speed");

                    b.Property<float>("Stamina");

                    b.Property<float>("Toughness");

                    b.Property<long>("UserId");

                    b.Property<float>("Vision");

                    b.Property<float>("Vitality");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Fighters");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Identity.Token", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Identity.Verification", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithOne()
                        .HasForeignKey("EmpoweredPixels.Models.Identity.Verification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.Match", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchRegistration", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithMany()
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Matches.Match", "Match")
                        .WithMany("Registrations")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchResult", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Matches.Match")
                        .WithOne()
                        .HasForeignKey("EmpoweredPixels.Models.Matches.MatchResult", "MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchScoreFighter", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter")
                        .WithMany()
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Matches.Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Roster.Fighter", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
