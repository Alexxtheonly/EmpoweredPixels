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
    [Migration("20191208175055_AddRarityToItem")]
    partial class AddRarityToItem
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

            modelBuilder.Entity("EmpoweredPixels.Models.Items.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Accuracy");

                    b.Property<int>("Agility");

                    b.Property<int>("Armor");

                    b.Property<int>("ConditionPower");

                    b.Property<int>("Enhancement");

                    b.Property<int>("Ferocity");

                    b.Property<Guid?>("FighterId");

                    b.Property<int>("HealingPower");

                    b.Property<int>("Level");

                    b.Property<int>("ParryChance");

                    b.Property<int>("Power");

                    b.Property<int>("Precision");

                    b.Property<int>("Rarity");

                    b.Property<int>("Speed");

                    b.Property<Guid>("Type");

                    b.Property<long>("UserId");

                    b.Property<int>("Vision");

                    b.Property<int>("Vitality");

                    b.HasKey("Id");

                    b.HasIndex("FighterId");

                    b.HasIndex("UserId");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Items.EquipmentOption", b =>
                {
                    b.Property<Guid>("EquipmentId");

                    b.Property<bool>("IsFavorite");

                    b.HasKey("EquipmentId");

                    b.ToTable("EquipmentOptions");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Items.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ItemId");

                    b.Property<int>("Rarity");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Items.SocketStone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Enhancement");

                    b.Property<Guid?>("EquipmentId");

                    b.Property<int>("Level");

                    b.Property<int>("Rarity");

                    b.Property<int>("Stat");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("UserId");

                    b.ToTable("SocketStones");
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
                            IsDeactivated = true
                        },
                        new
                        {
                            Id = 2,
                            IsDeactivated = true
                        },
                        new
                        {
                            Id = 3,
                            IsDeactivated = true
                        },
                        new
                        {
                            Id = 4,
                            IsDeactivated = false,
                            Name = "league.lastmanstanding",
                            Options = "{\"IntervalCron\":\"0 */2 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"273be142-200f-4bf4-bf2c-8308cc49701a\",\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"f5f16639-7796-40ee-b15b-f16eb6e946c4\",\"StaleCondition\":\"04616688-2cd1-4341-b757-afdae8af4035\"}}"
                        },
                        new
                        {
                            Id = 5,
                            IsDeactivated = false,
                            Name = "league.deathmatch",
                            Options = "{\"IntervalCron\":\"0 */5 * * *\",\"IsTeam\":false,\"TeamSize\":null,\"MatchOptions\":{\"IsPrivate\":true,\"MaxPowerlevel\":null,\"ActionsPerRound\":2,\"MaxFightersPerUser\":1,\"BotCount\":null,\"BotPowerlevel\":null,\"Features\":[\"273be142-200f-4bf4-bf2c-8308cc49701a\",\"0b93e657-ebf3-42f4-a049-fc9f7b70add9\",\"5237c31f-570a-42a6-8855-0ccdc2f351e1\",\"c34c17d6-550f-4bb1-bfc2-65d443deeb53\"],\"Battlefield\":\"dc937e88-f307-4cf0-aef5-b468d27aed4b\",\"Bounds\":\"fb1698b4-809b-40cd-94d6-0a3b257255c3\",\"PositionGenerator\":\"f88be549-9be0-4dd2-aabc-5af599dcc140\",\"MoveOrder\":\"12e9e0ae-eca3-440d-a649-48d687f6d97b\",\"WinCondition\":\"a9bfa4b5-df2d-4ca3-93b1-f6c721c4e8ff\",\"StaleCondition\":\"cc049ce5-13c5-4f1b-b679-f216eb7c27d9\"}}"
                        });
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Leagues.LeagueMatch", b =>
                {
                    b.Property<int>("LeagueId");

                    b.Property<Guid>("MatchId");

                    b.Property<int>("Division");

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

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchContribution", b =>
                {
                    b.Property<Guid>("FighterId");

                    b.Property<Guid>("MatchId");

                    b.Property<bool>("HasWon");

                    b.Property<bool>("IsSecond");

                    b.Property<bool>("IsThird");

                    b.Property<int>("KillsAndAssists");

                    b.Property<double>("MatchParticipation");

                    b.Property<double>("PercentageOfRoundsAlive");

                    b.HasKey("FighterId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("MatchContributions");
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

                    b.Property<byte[]>("RoundTicks");

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

                    b.Property<int>("RoundsAlive");

                    b.Property<Guid?>("TeamId");

                    b.Property<int>("TotalDamageDone");

                    b.Property<int>("TotalDamageTaken");

                    b.Property<int>("TotalDeaths");

                    b.Property<double>("TotalDistanceTraveled");

                    b.Property<int>("TotalEnergyUsed");

                    b.Property<int>("TotalKills");

                    b.Property<int>("TotalRegeneratedEnergy");

                    b.Property<int>("TotalRegeneratedHealth");

                    b.HasKey("MatchId", "FighterId");

                    b.HasIndex("FighterId");

                    b.ToTable("MatchScoreFighters");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchScoreTeam", b =>
                {
                    b.Property<Guid>("MatchId");

                    b.Property<Guid>("TeamId");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<int>("RoundsAlive");

                    b.Property<int>("TotalDamageDone");

                    b.Property<int>("TotalDamageTaken");

                    b.Property<int>("TotalDeaths");

                    b.Property<double>("TotalDistanceTraveled");

                    b.Property<int>("TotalEnergyUsed");

                    b.Property<int>("TotalKills");

                    b.Property<int>("TotalRegeneratedEnergy");

                    b.Property<int>("TotalRegeneratedHealth");

                    b.HasKey("MatchId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("MatchScoreTeams");
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

            modelBuilder.Entity("EmpoweredPixels.Models.Ratings.FighterEloRating", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentElo");

                    b.Property<Guid>("FighterId");

                    b.Property<DateTimeOffset?>("LastUpdate");

                    b.Property<int>("PreviousElo");

                    b.HasKey("Id");

                    b.HasIndex("FighterId")
                        .IsUnique();

                    b.ToTable("FighterEloRatings");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.Reward", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("Claimed");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<int?>("Level");

                    b.Property<Guid>("RewardPoolId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.RewardTier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Points");

                    b.Property<Guid>("RewardPoolId");

                    b.Property<int>("RewardTrackId");

                    b.HasKey("Id");

                    b.HasIndex("RewardTrackId");

                    b.ToTable("RewardTiers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Points = 200,
                            RewardPoolId = new Guid("6c70ddab-5b5c-4b1e-849f-78ceb7d14751"),
                            RewardTrackId = 1
                        },
                        new
                        {
                            Id = 2,
                            Points = 300,
                            RewardPoolId = new Guid("e620ff6f-e081-4588-b1e1-652f06808359"),
                            RewardTrackId = 1
                        },
                        new
                        {
                            Id = 3,
                            Points = 750,
                            RewardPoolId = new Guid("d00258c4-cb35-4ab3-bd00-bdb356bb6c2c"),
                            RewardTrackId = 1
                        },
                        new
                        {
                            Id = 4,
                            Points = 1000,
                            RewardPoolId = new Guid("b051e5c9-a679-489f-95c6-4e32aed2d15b"),
                            RewardTrackId = 1
                        });
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.RewardTrack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive");

                    b.Property<int>("TotalPoints");

                    b.HasKey("Id");

                    b.ToTable("RewardTracks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsActive = true,
                            TotalPoints = 10000
                        });
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.RewardTrackProgress", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<int>("RewardTrackId");

                    b.Property<DateTimeOffset>("Activated");

                    b.Property<DateTimeOffset?>("Completed");

                    b.Property<long>("Progress");

                    b.HasKey("UserId", "RewardTrackId");

                    b.HasIndex("RewardTrackId");

                    b.ToTable("RewardTrackProgresses");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Roster.Fighter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Accuracy");

                    b.Property<int>("Agility");

                    b.Property<int>("Armor");

                    b.Property<int>("ConditionPower");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<int>("Ferocity");

                    b.Property<int>("HealingPower");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("Level");

                    b.Property<string>("Name");

                    b.Property<int>("ParryChance");

                    b.Property<int>("Power");

                    b.Property<int>("Precision");

                    b.Property<int>("Speed");

                    b.Property<long>("UserId");

                    b.Property<int>("Vision");

                    b.Property<int>("Vitality");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Fighters");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Roster.FighterConfiguration", b =>
                {
                    b.Property<Guid>("FighterId");

                    b.Property<Guid?>("AttunementId");

                    b.Property<float>("HealThreshold");

                    b.HasKey("FighterId");

                    b.ToTable("FighterConfigurations");
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Roster.FighterExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("FighterId");

                    b.Property<DateTimeOffset?>("LastUpdate");

                    b.Property<long>("Points");

                    b.HasKey("Id");

                    b.HasIndex("FighterId")
                        .IsUnique();

                    b.ToTable("FighterExperiences");
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

            modelBuilder.Entity("EmpoweredPixels.Models.Items.Equipment", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithMany("Equipment")
                        .HasForeignKey("FighterId");

                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Items.EquipmentOption", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Items.Equipment", "Equipment")
                        .WithOne("Option")
                        .HasForeignKey("EmpoweredPixels.Models.Items.EquipmentOption", "EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Items.Item", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Items.SocketStone", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Items.Equipment", "Equipment")
                        .WithMany("SocketStones")
                        .HasForeignKey("EquipmentId");

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

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchContribution", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithMany()
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Matches.Match", "Match")
                        .WithMany("MatchContributions")
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
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithMany()
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Matches.Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Matches.MatchScoreTeam", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Matches.Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EmpoweredPixels.Models.Matches.MatchTeam")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Ratings.FighterEloRating", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithOne("EloRating")
                        .HasForeignKey("EmpoweredPixels.Models.Ratings.FighterEloRating", "FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.Reward", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.RewardTier", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Rewards.RewardTrack", "RewardTrack")
                        .WithMany("Tiers")
                        .HasForeignKey("RewardTrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Rewards.RewardTrackProgress", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Rewards.RewardTrack", "RewardTrack")
                        .WithMany()
                        .HasForeignKey("RewardTrackId")
                        .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity("EmpoweredPixels.Models.Roster.FighterConfiguration", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithOne("Configuration")
                        .HasForeignKey("EmpoweredPixels.Models.Roster.FighterConfiguration", "FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EmpoweredPixels.Models.Roster.FighterExperience", b =>
                {
                    b.HasOne("EmpoweredPixels.Models.Roster.Fighter", "Fighter")
                        .WithOne()
                        .HasForeignKey("EmpoweredPixels.Models.Roster.FighterExperience", "FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
