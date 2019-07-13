using EmpoweredPixels.Models.Identity;
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
    }
  }
}
