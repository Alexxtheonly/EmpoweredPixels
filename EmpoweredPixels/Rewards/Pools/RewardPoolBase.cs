using System;
using System.Collections.Generic;
using EmpoweredPixels.Models.Rewards;

namespace EmpoweredPixels.Rewards.Pools
{
  public abstract class RewardPoolBase<T>
  {
    protected static readonly Random Random = new Random();

    protected abstract Dictionary<Guid, PoolItemOption> ItemOptions { get; }

    public abstract IEnumerable<T> Claim(Reward reward);

    protected class PoolItemOption
    {
      public int QuantityMin { get; set; }

      public int QuantityMax { get; set; }
    }
  }
}
