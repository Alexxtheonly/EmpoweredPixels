using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Factories.Rewards;
using EmpoweredPixels.Models;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public class SeasonRewardClaimer : ISeasonRewardClaimer
  {
    private readonly IRewardFactory rewardFactory;
    private readonly IDateTimeProvider dateTimeProvider;

    public SeasonRewardClaimer(IRewardFactory rewardFactory, IDateTimeProvider dateTimeProvider)
    {
      this.rewardFactory = rewardFactory;
      this.dateTimeProvider = dateTimeProvider;
    }

    public async Task ClaimAsync(DatabaseContext context, long userId)
    {
      var rewards = await context.Rewards
        .Where(o => o.UserId == userId)
        .AsTracking()
        .Where(o => o.Claimed == null)
        .ToListAsync();

      foreach (var reward in rewards)
      {
        reward.Claimed = dateTimeProvider.Now;
        context.AddRange(rewardFactory.Claim(reward).ToList());
      }
    }
  }
}
