using System;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.RewardTrackCalculation
{
  public class RewardTrackCalculator : IRewardTrackCalculator
  {
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly DatabaseContext context;

    public RewardTrackCalculator(IDateTimeProvider dateTimeProvider, DatabaseContext context)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.context = context;
    }

    public async Task Calculate(Fighter fighter, int points)
    {
      var currentProgress = await context.RewardTrackProgresses
        .AsTracking()
        .Where(o => o.Completed == null)
        .OrderByDescending(o => o.Activated)
        .FirstOrDefaultAsync(o => o.UserId == fighter.UserId);

      if (currentProgress == null)
      {
        currentProgress = await CreateNewProgress(fighter.UserId);
        context.RewardTrackProgresses.Add(currentProgress);
      }

      var track = await context.RewardTracks
        .Include(o => o.Tiers)
        .FirstOrDefaultAsync(o => o.Id == currentProgress.RewardTrackId);
      if (track == null)
      {
        throw new ArgumentNullException(nameof(track));
      }

      // o.Points > currentProgress.Progress && o.Points <= (currentProgress.Progress + points)
      var reachedTiers = track.Tiers.Where(o => (currentProgress.Progress + points).NearestBase(o.Points) > currentProgress.Progress);

      var level = fighter.Level.NearestBase(8);
      if (level == 0)
      {
        level = 1;
      }

      foreach (var tier in reachedTiers)
      {
        context.Rewards.Add(new Reward()
        {
          UserId = fighter.UserId,
          Level = level,
          Created = dateTimeProvider.Now,
          RewardPoolId = tier.RewardPoolId,
        });
      }

      currentProgress.Progress += points;
      await context.SaveChangesAsync();
    }

    private async Task<RewardTrackProgress> CreateNewProgress(long userId)
    {
      var track = await context.RewardTracks.OrderByDescending(o => o.TotalPoints).FirstOrDefaultAsync();
      if (track == null)
      {
        throw new ArgumentNullException(nameof(track));
      }

      return new RewardTrackProgress()
      {
        UserId = userId,
        Activated = dateTimeProvider.Now,
        RewardTrackId = track.Id,
      };
    }
  }
}
