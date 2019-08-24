using System.Collections.Generic;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Rewards;

namespace EmpoweredPixels.Factories.Rewards
{
  public interface IRewardFactory
  {
    IEnumerable<Item> Claim(Reward reward);
  }
}
