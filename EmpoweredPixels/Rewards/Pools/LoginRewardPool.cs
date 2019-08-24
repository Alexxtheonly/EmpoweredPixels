using System;
using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Rewards.Pools
{
  public class LoginRewardPool : RewardPoolBase
  {
    public static readonly Guid Id = new Guid("206128E9-2A59-449D-AE2E-61DB923643C0");

    protected override Dictionary<Guid, PoolItemOption> ItemOptions => new Dictionary<Guid, PoolItemOption>()
    {
      [EmpoweredParticle.Id] = new PoolItemOption()
      {
        QuantityMin = 5,
        QuantityMax = 20,
      }
    };

    public override IEnumerable<Item> Claim(Reward reward)
    {
      if (reward.RewardPoolId != Id)
      {
        throw new ArgumentException("wrong reward pool");
      }

      var item = ItemOptions.Random(Random);
      var count = Random.Next(item.Value.QuantityMin, item.Value.QuantityMax + 1);

      for (int i = 0; i < count; i++)
      {
        yield return new Item()
        {
          ItemId = item.Key,
          UserId = reward.UserId,
        };
      }
    }
  }
}
