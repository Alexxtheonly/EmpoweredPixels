using System;
using System.Collections.Generic;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Rewards.Pools;

namespace EmpoweredPixels.Factories.Rewards
{
  public class RewardFactory : IRewardFactory
  {
    private static readonly Dictionary<Guid, RewardPoolBase> RewardPools = new Dictionary<Guid, RewardPoolBase>()
    {
      [LoginRewardPool.Id] = new LoginRewardPool(),
    };

    public IEnumerable<Item> Claim(Reward reward)
    {
      if (RewardPools.TryGetValue(reward.RewardPoolId, out RewardPoolBase pool))
      {
        return pool.Claim(reward);
      }

      throw new Exception("Rewardpool not found.");
    }
  }
}
