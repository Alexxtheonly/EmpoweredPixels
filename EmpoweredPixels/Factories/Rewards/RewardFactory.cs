using System;
using System.Collections.Generic;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Rewards;
using EmpoweredPixels.Rewards.Pools;
using EmpoweredPixels.Rewards.Pools.Chests;
using EmpoweredPixels.Rewards.Pools.FighterLevelUp;
using EmpoweredPixels.Utilities.EquipmentGeneration;

namespace EmpoweredPixels.Factories.Rewards
{
  public class RewardFactory : IRewardFactory
  {
    private static readonly Dictionary<Guid, RewardPoolBase<Item>> ItemPools = new Dictionary<Guid, RewardPoolBase<Item>>()
    {
      [LoginRewardPool.Id] = new LoginRewardPool(),
    };

    private readonly Dictionary<Guid, RewardPoolBase<Equipment>> equipmentPools;

    public RewardFactory(IEquipmentGenerator equipmentGenerator)
    {
      equipmentPools = new Dictionary<Guid, RewardPoolBase<Equipment>>()
      {
        [FighterLevelRewardPool.Id] = new FighterLevelRewardPool(equipmentGenerator),
        [EmpoweredChestRewardPool.Common] = new EmpoweredChestRewardPool(equipmentGenerator) { Rarity = ItemRarity.Common, },
        [EmpoweredChestRewardPool.Rare] = new EmpoweredChestRewardPool(equipmentGenerator) { Rarity = ItemRarity.Rare, },
        [EmpoweredChestRewardPool.Fabled] = new EmpoweredChestRewardPool(equipmentGenerator) { Rarity = ItemRarity.Fabled, },
        [EmpoweredChestRewardPool.Mythic] = new EmpoweredChestRewardPool(equipmentGenerator) { Rarity = ItemRarity.Mythic },
      };
    }

    public IEnumerable<IReward> Claim(Reward reward)
    {
      if (ItemPools.TryGetValue(reward.RewardPoolId, out RewardPoolBase<Item> itemPool))
      {
        return itemPool.Claim(reward);
      }

      if (equipmentPools.TryGetValue(reward.RewardPoolId, out RewardPoolBase<Equipment> equipmentPool))
      {
        return equipmentPool.Claim(reward);
      }

      throw new Exception("Rewardpool not found.");
    }
  }
}
