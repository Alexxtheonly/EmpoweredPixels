using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Rewards.Pools;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Jobs.Rewards
{
  public class LoginRewardJob : ILoginRewardJob
  {
    private readonly DatabaseContext context;
    private readonly IDateTimeProvider dateTimeProvider;

    public LoginRewardJob(DatabaseContext context, IDateTimeProvider dateTimeProvider)
    {
      this.context = context;
      this.dateTimeProvider = dateTimeProvider;
    }

    public async Task CreateLoginRewards()
    {
      var latestRewards = context.Rewards
        .Where(o => o.RewardPoolId == LoginRewardPool.Id)
        .GroupBy(o => o.UserId)
        .Select(o => o.OrderByDescending(u => u.Created).First());

      var eligableUsers = await context.Users
        .GroupJoin(latestRewards, o => o.Id, o => o.UserId, (x, y) => new { User = x, Reward = y })
        .SelectMany(x => x.Reward.DefaultIfEmpty(), (x, y) => new { x.User, Reward = y })
        .Where(o => o.Reward == null || o.Reward.Claimed != null)
        .ToListAsync();

      foreach (var user in eligableUsers.Select(o => o.User))
      {
        context.Rewards.Add(new Reward()
        {
          RewardPoolId = LoginRewardPool.Id,
          Created = dateTimeProvider.Now,
          UserId = user.Id,
        });
      }

      await context.SaveChangesAsync();
    }
  }
}
