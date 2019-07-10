using System;
using Microsoft.EntityFrameworkCore;
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
  }
}
