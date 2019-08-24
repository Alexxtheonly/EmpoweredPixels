using System;
using System.Collections.Generic;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Rewards.Pools
{
  public abstract class RewardPoolBase
  {
    protected static readonly Random Random = new Random();

    protected abstract Dictionary<Guid, PoolItemOption> ItemOptions { get; }

    public abstract IEnumerable<Item> Claim(Reward reward);

    protected class PoolItemOption
    {
      public int QuantityMin { get; set; }

      public int QuantityMax { get; set; }
    }
  }
}
