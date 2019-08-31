﻿// <auto-generated />
using System;
using EmpoweredPixels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmpoweredPixels.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190831134840_UpdateLeagues")]
    partial class UpdateLeagues
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("EmpoweredPixels.Models.Items.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ItemId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Leagues.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeactivated");

                    b.Property<string>("Name");

                    b.Property<string>("Options");

                    b.HasKey("Id");

                    b.ToTable("Leagues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeactivated = false,
                            Name = "League 300",
                            Options = "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":300,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\",\"732a2a25-97a6-4fa0-ae65-96a503f9a1ea\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"86b56a4f-77f4-4624-b67e-1887e77039a0\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}"
                        },
                        new
                        {
                            Id = 2,
                            IsDeactivated = false,
                            Name = "League 500",
                            Options = "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":500,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\",\"732a2a25-97a6-4fa0-ae65-96a503f9a1ea\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}"
                        },
                        new
                        {
                            Id = 3,
                            IsDeactivated = false,
                            Name = "League 750",
                            Options = "{\"IntervalCron\":\"0 */3 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":750,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"e800723c-6324-47ab-9593-1952346ad772\",\"77c70366-24fb-4af3-8a34-869f930bc420\",\"732a2a25-97a6-4fa0-ae65-96a503f9a1ea\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}"
                        });
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Leagues.LeagueMatch", b =>
                {
                    b.Property<int>("LeagueId");

                    b.Property<Guid>("MatchId");

                    b.HasKey("LeagueId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("LeagueMatches");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Leagues.LeagueSubscription", b =>
                {
                    b.Property<int>("LeagueId");

                    b.Property<Guid>("FighterId");

                    b.HasKey("LeagueId", "FighterId");

                    b.HasIndex("FighterId");

                    b.ToTable("LeagueSubscriptions");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Created");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Options");

                    b.Property<DateTimeOffset?>("Started");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchFighterResult", b =>
                {
                    b.Property<Guid>("MatchId");

                    b.Property<Guid>("FighterId");

                    b.Property<int>("Position");

                    b.Property<short>("Result");

                    b.HasKey("MatchId", "FighterId");

                    b.HasIndex("FighterId");

                    b.HasIndex("Result");

                    b.ToTable("MatchFighterResults");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchRegistration", b =>
                {
                    b.Property<Guid>("MatchId");

                    b.Property<Guid>("FighterId");

                    b.Property<DateTimeOffset>("Date");

                    b.Property<Guid?>("TeamId");

                    b.HasKey("MatchId", "FighterId");

                    b.HasIndex("FighterId");

                    b.HasIndex("TeamId");

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

                    b.Property<int>("Powerlevel");

                    b.Property<int>("RoundsAlive");

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

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MatchId");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchTeams");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.Reward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Claimed");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<Guid>("RewardPoolId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rewards");
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

            modelBuilder.Entity("EmpoweredPixels.Models.Items.Item", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Leagues.LeagueMatch", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Leagues.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Matches.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Leagues.LeagueSubscription", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithMany()
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Leagues.League", "League")
                        .WithMany("Subscriptions")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.Match", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchFighterResult", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithMany()
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Matches.Match", "Match")
                        .WithMany("MatchFighterResults")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
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

                    b.HasOne("EmpoweredPixels.Models.Matches.MatchTeam", "Team")
                        .WithMany("Registrations")
                        .HasForeignKey("TeamId")
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

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.Reward", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
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
