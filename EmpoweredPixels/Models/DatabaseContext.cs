using System;
using EmpoweredPixels.DataTransferObjects.Leagues;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Ratings;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Rewards.Pools.Chests;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharpFightingEngine.Battlefields.Constants;
using SharpFightingEngine.Constants;

namespace EmpoweredPixels.Models
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
      : base(options)
    {
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Token> Tokens { get; set; }

    public DbSet<Verification> Verifications { get; set; }

    public DbSet<Fighter> Fighters { get; set; }

    public DbSet<FighterExperience> FighterExperiences { get; set; }

    public DbSet<Match> Matches { get; set; }

    public DbSet<MatchRegistration> MatchRegistrations { get; set; }

    public DbSet<MatchContribution> MatchContributions { get; set; }

    public DbSet<MatchTeam> MatchTeams { get; set; }

    public DbSet<MatchScoreFighter> MatchScoreFighters { get; set; }

    public DbSet<MatchResult> MatchResults { get; set; }

    public DbSet<MatchScoreTeam> MatchScoreTeams { get; set; }

    public DbSet<League> Leagues { get; set; }

    public DbSet<LeagueMatch> LeagueMatches { get; set; }

    public DbSet<LeagueSubscription> LeagueSubscriptions { get; set; }

    public DbSet<Reward> Rewards { get; set; }

    public DbSet<RewardTrack> RewardTracks { get; set; }

    public DbSet<RewardTier> RewardTiers { get; set; }

    public DbSet<RewardTrackProgress> RewardTrackProgresses { get; set; }

    public DbSet<Item> Items { get; set; }

    public DbSet<Equipment> Equipment { get; set; }

    public DbSet<SocketStone> SocketStones { get; set; }

    public DbSet<FighterEloRating> FighterEloRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      if (modelBuilder == null)
      {
        throw new ArgumentNullException(nameof(modelBuilder));
      }

      modelBuilder.Entity<User>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasIndex(o => o.Email).IsUnique();
        e.HasIndex(o => o.Name).IsUnique();

        e.HasMany<Match>().WithOne(o => o.User).HasForeignKey(o => o.CreatorUserId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
      });

      modelBuilder.Entity<Token>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Verification>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithOne().HasForeignKey<Verification>(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Fighter>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
        e.HasQueryFilter(o => !o.IsDeleted);
      });

      modelBuilder.Entity<FighterExperience>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.Fighter).WithOne().HasForeignKey<FighterExperience>(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Match>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.Property(o => o.Options).HasConversion(
          o => JsonConvert.SerializeObject(o),
          o => JsonConvert.DeserializeObject<MatchOptionsDto>(o));
      });

      modelBuilder.Entity<MatchRegistration>(e =>
      {
        e.HasKey(o => new { o.MatchId, o.FighterId });
        e.HasOne(o => o.Match).WithMany(o => o.Registrations).HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Fighter).WithMany().HasForeignKey(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Team).WithMany(o => o.Registrations).HasForeignKey(o => o.TeamId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
      });

      modelBuilder.Entity<MatchTeam>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasIndex(o => o.MatchId);
      });

      modelBuilder.Entity<MatchScoreFighter>(e =>
      {
        e.HasKey(o => new { o.MatchId, o.FighterId });
        e.HasOne(o => o.Fighter).WithMany().HasForeignKey(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne<Match>().WithMany().HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<MatchScoreTeam>(e =>
      {
        e.HasKey(o => new { o.MatchId, o.TeamId });
        e.HasOne<MatchTeam>().WithMany().HasForeignKey(o => o.TeamId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne<Match>().WithMany().HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<MatchResult>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne<Match>().WithOne().HasForeignKey<MatchResult>(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<MatchContribution>(e =>
      {
        e.HasKey(o => new { o.FighterId, o.MatchId });
        e.HasOne(o => o.Fighter).WithMany().HasForeignKey(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Match).WithMany(o => o.MatchContributions).HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<League>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.Property(o => o.Options).HasConversion(
          o => JsonConvert.SerializeObject(o),
          o => JsonConvert.DeserializeObject<LeagueOptionsDto>(o));
        e.HasQueryFilter(o => !o.IsDeactivated);
        e.HasData(
          new League()
          {
            Id = 1,
            IsDeactivated = true,
          }, new League()
          {
            Id = 2,
            IsDeactivated = true,
          }, new League()
          {
            Id = 3,
            IsDeactivated = true,
          },
          new League()
          {
            Id = 4,
            Name = "league.lastmanstanding",
            Options = new LeagueOptionsDto()
            {
              IntervalCron = "0 */2 * * *",
              MatchOptions = new MatchOptionsDto()
              {
                ActionsPerRound = 2,
                Battlefield = BattlefieldConstants.Plain,
                Bounds = BoundsConstants.Small,
                IsPrivate = true,
                MaxFightersPerUser = 1,
                Features = new Guid[]
                {
                  FeatureConstants.ApplyCondition,
                  FeatureConstants.ApplyBuff,
                },
                MoveOrder = MoveOrderConstants.AllRandom,
                PositionGenerator = FighterPositionGeneratorConstants.AllRandom,
                StaleCondition = StaleConditionConstants.NoWinnerCanBeDetermined,
                WinCondition = WinConditionConstants.LastManStanding,
              }
            }
          },
          new League()
          {
            Id = 5,
            Name = "league.deathmatch",
            Options = new LeagueOptionsDto()
            {
              IntervalCron = "0 */5 * * *",
              MatchOptions = new MatchOptionsDto()
              {
                ActionsPerRound = 2,
                Battlefield = BattlefieldConstants.Plain,
                Bounds = BoundsConstants.Small,
                IsPrivate = true,
                MaxFightersPerUser = 1,
                Features = new Guid[]
                {
                  FeatureConstants.ApplyBuff,
                  FeatureConstants.ApplyCondition,
                  FeatureConstants.ReviveDeadFighters,
                },
                MoveOrder = MoveOrderConstants.AllRandom,
                PositionGenerator = FighterPositionGeneratorConstants.AllRandom,
                StaleCondition = StaleConditionConstants.None,
                WinCondition = WinConditionConstants.FiftyRounds,
              },
            },
          });
      });

      modelBuilder.Entity<LeagueMatch>(e =>
      {
        e.HasKey(o => new { o.LeagueId, o.MatchId });
        e.HasOne(o => o.League).WithMany().HasForeignKey(o => o.LeagueId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Match).WithMany().HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<LeagueSubscription>(e =>
      {
        e.HasKey(o => new { o.LeagueId, o.FighterId });
        e.HasOne(o => o.League).WithMany(o => o.Subscriptions).HasForeignKey(o => o.LeagueId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Fighter).WithMany().HasForeignKey(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Reward>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<RewardTrack>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasQueryFilter(o => o.IsActive);
        e.HasData(
          new RewardTrack()
          {
            Id = 1,
            IsActive = true,
            TotalPoints = 10000,
          });
      });

      modelBuilder.Entity<RewardTier>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.RewardTrack).WithMany(o => o.Tiers).HasForeignKey(o => o.RewardTrackId).OnDelete(DeleteBehavior.Cascade);
        SeedRewardTiers(e);
      });

      modelBuilder.Entity<RewardTrackProgress>(e =>
      {
        e.HasKey(o => new { o.UserId, o.RewardTrackId });
        e.HasOne(o => o.RewardTrack).WithMany().HasForeignKey(o => o.RewardTrackId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Item>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<Equipment>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Fighter).WithMany(o => o.Equipment).HasForeignKey(o => o.FighterId).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull);
      });

      modelBuilder.Entity<SocketStone>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Equipment).WithMany(o => o.SocketStones).HasForeignKey(o => o.EquipmentId).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull);
      });

      modelBuilder.Entity<FighterEloRating>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.Fighter).WithOne(o => o.EloRating).HasForeignKey<FighterEloRating>(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
      });
    }

    private static void SeedRewardTiers(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RewardTier> e)
    {
      e.HasData(
                new RewardTier()
                {
                  Id = 1,
                  Points = 200,
                  RewardTrackId = 1,
                  RewardPoolId = EmpoweredChestRewardPool.Common,
                },
                new RewardTier()
                {
                  Id = 2,
                  Points = 300,
                  RewardTrackId = 1,
                  RewardPoolId = EmpoweredChestRewardPool.Rare,
                },
                new RewardTier()
                {
                  Id = 3,
                  Points = 750,
                  RewardTrackId = 1,
                  RewardPoolId = EmpoweredChestRewardPool.Fabled,
                },
                new RewardTier()
                {
                  Id = 4,
                  Points = 1000,
                  RewardTrackId = 1,
                  RewardPoolId = EmpoweredChestRewardPool.Mythic,
                });
    }
  }
}
