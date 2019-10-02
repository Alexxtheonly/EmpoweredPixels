using System.Collections.Generic;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Rewards;

namespace EmpoweredPixels.Factories.Rewards
{
  public interface IRewardFactory
  {
    IEnumerable<IReward> Claim(Reward reward);
  }
}
