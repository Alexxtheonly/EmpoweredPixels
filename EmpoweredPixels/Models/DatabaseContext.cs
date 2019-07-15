using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Roster;
using Microsoft.EntityFrameworkCore;

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

    public DbSet<MatchScoreFighter> MatchScoreFighters { get; set; }

    public DbSet<MatchResult> MatchResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasIndex(o => o.Email).IsUnique();
        e.HasIndex(o => o.Name).IsUnique();
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
      });

      modelBuilder.Entity<MatchRegistration>(e =>
      {
        e.HasKey(o => new { o.MatchId, o.FighterId });
        e.HasOne(o => o.Match).WithMany(o => o.Registrations).HasForeignKey(o => o.MatchId).OnDelete(DeleteBehavior.Cascade);
        e.HasOne(o => o.Fighter).WithMany().HasForeignKey(o => o.FighterId).OnDelete(DeleteBehavior.Cascade);
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
    }
  }
}
