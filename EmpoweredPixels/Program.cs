using EmpoweredPixels.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EmpoweredPixels
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args)
        .Build()
        .MigrateDatabase()
        .Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
    }
  }
}
