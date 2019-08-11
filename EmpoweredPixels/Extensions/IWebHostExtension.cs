using EmpoweredPixels.Jobs;
using EmpoweredPixels.Models;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Extensions
{
  public static class IWebHostExtension
  {
    public static IWebHost MigrateDatabase(this IWebHost webHost)
    {
      var serviceScopeFactory = webHost.Services.GetService<IServiceScopeFactory>();

      using (var scope = serviceScopeFactory.CreateScope())
      {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DatabaseContext>();

        if (context.Database.IsSqlServer())
        {
          context.Database.Migrate();
        }
      }

      return webHost;
    }
  }
}
