﻿using System.Linq;
using EmpoweredPixels.Jobs;
using EmpoweredPixels.Jobs.Inventory;
using EmpoweredPixels.Jobs.Rewards;
using EmpoweredPixels.Jobs.Seasons;
using EmpoweredPixels.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Extensions
{
  public static class RecurringJobManagerExtension
  {
    public static void AddLeagueJobs(this IRecurringJobManager jobManager, DatabaseContext context)
    {
      var leagues = context.Leagues
        .IgnoreQueryFilters()
        .ToList();

      foreach (var league in leagues.Where(o => !o.IsDeactivated))
      {
        jobManager.AddOrUpdate<ILeagueJob>(league.Id.ToString(), o => o.RunMatchAsync(league.Id), league.Options.IntervalCron);
      }

      foreach (var league in leagues.Where(o => o.IsDeactivated))
      {
        jobManager.RemoveIfExists(league.Id.ToString());
      }
    }

    public static void AddLoginRewardJob(this IRecurringJobManager jobManager)
    {
      jobManager.AddOrUpdate<ILoginRewardJob>("loginrewardjob", o => o.CreateLoginRewards(), "0 0 * * *");
    }

    public static void AddSeasonJob(this IRecurringJobManager jobManager)
    {
      jobManager.AddOrUpdate<ISeasonInitiatorJob>(nameof(ISeasonInitiatorJob), o => o.InitAsync(), "30 * * * *");
    }

    public static void AddRemoveExcessParticlesJob(this IRecurringJobManager jobManager)
    {
      jobManager.AddOrUpdate<IRemoveExcessParticlesJob>(nameof(IRemoveExcessParticlesJob), o => o.RemoveAsync(), "*/10 * * * *");
    }
  }
}
