using System;
using EmpoweredPixels.DataTransferObjects.Leagues;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Models.Roster;
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

    public DbSet<Match> Matches { get; set; }

    public DbSet<MatchRegistration> MatchRegistrations { get; set; }

    public DbSet<MatchTeam> MatchTeams { get; set; }

    public DbSet<MatchScoreFighter> MatchScoreFighters { get; set; }

    public DbSet<MatchResult> MatchResults { get; set; }

    public DbSet<MatchFighterResult> MatchFighterResults { get; set; }

    public DbSet<League> Leagues { get; set; }

    public DbSet<LeagueMatch> LeagueMatches { get; set; }

    public DbSet<LeagueSubscription> LeagueSubscriptions { get; set; }

    public DbSet<Reward> Rewards { get; set; }

    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
        e.HasOne<Fighter>().WithMany().HasForeignKey(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne<Match>().WithMany().HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<MatchResult>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne<Match>().WithOne().HasForeignKey<MatchResult>(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
      });

      modelBuilder.Entity<MatchFighterResult>(e =>
      {
        e.HasKey(o => new { o.MatchId, o.FighterId });
        e.HasOne(o => o.Match).WithMany(o => o.MatchFighterResults).HasForeignKey(o => o.MatchId);
        e.HasOne(o => o.Fighter).WithMany().HasForeignKey(o => o.FighterId);
        e.HasIndex(o => o.Result);
      });

      modelBuilder.Entity<League>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.Property(o => o.Options).HasConversion(
          o => JsonConvert.SerializeObject(o),
          o => JsonConvert.DeserializeObject<LeagueOptionsDto>(o));

        e.HasData(
          new League()
          {
            Id = 1,
            Name = "League 300",
            Options = new LeagueOptionsDto()
            {
              IntervalCron = "0 */3 * * *",
              IsTeam = false,
              MatchOptions = new MatchOptionsDto()
              {
                ActionsPerRound = 2,
                MaxFightersPerUser = 1,
                MaxPowerlevel = 300,
                Battlefield = BattlefieldConstants.Plain,
                Bounds = BoundsConstants.Tiny,
                IsPrivate = true,
                Features = new Guid[]
                {
                  FeatureConstants.ApplyCondition,
                  FeatureConstants.RegenerateHealth,
                  FeatureConstants.RegenerateEnergy,
                  FeatureConstants.SacrificeToEntity,
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
            Id = 2,
            Name = "League 500",
            Options = new LeagueOptionsDto()
            {
              IntervalCron = "0 */3 * * *",
              IsTeam = false,
              MatchOptions = new MatchOptionsDto()
              {
                ActionsPerRound = 2,
                MaxFightersPerUser = 1,
                MaxPowerlevel = 500,
                Battlefield = BattlefieldConstants.Plain,
                Bounds = BoundsConstants.Small,
                IsPrivate = true,
                Features = new Guid[]
                {
                  FeatureConstants.ApplyCondition,
                  FeatureConstants.RegenerateHealth,
                  FeatureConstants.RegenerateEnergy,
                  FeatureConstants.SacrificeToEntity,
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
            Id = 3,
            Name = "League 750",
            Options = new LeagueOptionsDto()
            {
              IntervalCron = "0 */3 * * *",
              IsTeam = false,
              MatchOptions = new MatchOptionsDto()
              {
                ActionsPerRound = 2,
                MaxFightersPerUser = 1,
                MaxPowerlevel = 750,
                Battlefield = BattlefieldConstants.Plain,
                Bounds = BoundsConstants.Small,
                IsPrivate = true,
                Features = new Guid[]
                {
                  FeatureConstants.ApplyCondition,
                  FeatureConstants.RegenerateHealth,
                  FeatureConstants.RegenerateEnergy,
                  FeatureConstants.SacrificeToEntity,
                },
                MoveOrder = MoveOrderConstants.AllRandom,
                PositionGenerator = FighterPositionGeneratorConstants.AllRandom,
                StaleCondition = StaleConditionConstants.NoWinnerCanBeDetermined,
                WinCondition = WinConditionConstants.LastManStanding,
              }
            }
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

      modelBuilder.Entity<Item>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasOne(o => o.User).WithMany().HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
      });
    }
  }
}
