using EmpoweredPixels.Jobs;
using EmpoweredPixels.Models;
using Hangfire;

namespace EmpoweredPixels.Extensions
{
  public static class RecurringJobManagerExtension
  {
    public static void AddLeagueJobs(this IRecurringJobManager jobManager, DatabaseContext context)
    {
      foreach (var league in context.Leagues)
      {
        jobManager.AddOrUpdate<ILeagueJob>(league.Id.ToString(), o => o.RunMatchAsync(league.Id), league.Options.IntervalCron);
      }
    }
  }
}
