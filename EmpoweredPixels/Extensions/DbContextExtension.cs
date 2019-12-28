using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmpoweredPixels.Extensions
{
  public static class DbContextExtension
  {
    /// <summary>
    /// Configure the options to use the in-memory-database
    /// </summary>
    /// <param name="builder">Context builder</param>
    /// <returns>returns <cref="DbContextOptionsBuilder"></returns>
    public static DbContextOptionsBuilder ConfigureInMemoryDatabase(this DbContextOptionsBuilder builder)
    {
      builder
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .UseInternalServiceProvider(
        new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider());

      return builder;
    }

    public static DbContextOptionsBuilder ConfigureSqlServerDatabase(this DbContextOptionsBuilder builder, string connectionString)
    {
      return builder
         .UseSqlServer(connectionString, cfg =>
         {
           cfg.EnableRetryOnFailure();
           cfg.CommandTimeout(180);
         });
    }

    public static DbContextOptionsBuilder ConfigureDatabase(this DbContextOptionsBuilder builder, IConfiguration configuration)
    {
      var connectionString = configuration.GetConnectionString();
      if (string.IsNullOrEmpty(connectionString))
      {
        return builder.ConfigureInMemoryDatabase();
      }
      else
      {
        return builder
          .ConfigureSqlServerDatabase(connectionString);
      }
    }
  }
}
