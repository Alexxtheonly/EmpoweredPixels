using System;
using EmpoweredPixels.DataTransferObjects.Leagues;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Roster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
        e.HasKey(o => new { o.FighterId, o.MatchId });
        e.HasOne<Match>().WithMany().HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne<Fighter>().WithMany().HasForeignKey(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
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
                Battlefield = new Guid("DC937E88-F307-4CF0-AEF5-B468D27AED4B"),
                Bounds = new Guid("FB1698B4-809B-40CD-94D6-0A3B257255C3"),
                IsPrivate = true,
                Features = new Guid[]
                {
                  new Guid("E800723C-6324-47AB-9593-1952346AD772"),
                  new Guid("77C70366-24FB-4AF3-8A34-869F930BC420"),
                },
                MoveOrder = new Guid("12E9E0AE-ECA3-440D-A649-48D687F6D97B"),
                PositionGenerator = new Guid("F88BE549-9BE0-4DD2-AABC-5AF599DCC140"),
                StaleCondition = new Guid("04616688-2CD1-4341-B757-AFDAE8AF4035"),
                WinCondition = new Guid("F5F16639-7796-40EE-B15B-F16EB6E946C4"),
              }
            }
          },
          new League()
          {
            Id = 1,
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
                Battlefield = new Guid("DC937E88-F307-4CF0-AEF5-B468D27AED4B"),
                Bounds = new Guid("FB1698B4-809B-40CD-94D6-0A3B257255C3"),
                IsPrivate = true,
                Features = new Guid[]
                {
                  new Guid("E800723C-6324-47AB-9593-1952346AD772"),
                  new Guid("77C70366-24FB-4AF3-8A34-869F930BC420"),
                },
                MoveOrder = new Guid("12E9E0AE-ECA3-440D-A649-48D687F6D97B"),
                PositionGenerator = new Guid("F88BE549-9BE0-4DD2-AABC-5AF599DCC140"),
                StaleCondition = new Guid("04616688-2CD1-4341-B757-AFDAE8AF4035"),
                WinCondition = new Guid("F5F16639-7796-40EE-B15B-F16EB6E946C4"),
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
    }
  }
}
