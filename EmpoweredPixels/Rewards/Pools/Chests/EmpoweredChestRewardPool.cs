using System;
using System.Collections.Generic;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.EquipmentGeneration;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Rewards.Pools.Chests
{
  public class EmpoweredChestRewardPool : RewardPoolBase<Equipment>
  {
    public static readonly Guid Common = new Guid("6C70DDAB-5B5C-4B1E-849F-78CEB7D14751");
    public static readonly Guid Rare = new Guid("E620FF6F-E081-4588-B1E1-652F06808359");
    public static readonly Guid Fabled = new Guid("D00258C4-CB35-4AB3-BD00-BDB356BB6C2C");
    public static readonly Guid Mythic = new Guid("B051E5C9-A679-489F-95C6-4E32AED2D15B");

    private readonly IEquipmentGenerator equipmentGenerator;

    public EmpoweredChestRewardPool(IEquipmentGenerator equipmentGenerator)
    {
      this.equipmentGenerator = equipmentGenerator;
    }

    public ItemRarity Rarity { get; set; }

    public int Count { get; set; } = 1;

    protected override Dictionary<Guid, PoolItemOption> ItemOptions => new Dictionary<Guid, PoolItemOption>()
    {
      [EquipmentConstants.ArmorChest] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
      [EquipmentConstants.ArmorHead] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
      [EquipmentConstants.ArmorShoulders] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
      [EquipmentConstants.ArmorHands] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
      [EquipmentConstants.ArmorLegs] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
      [EquipmentConstants.ArmorShoes] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
      [EquipmentConstants.WeaponGreatsword] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
      [EquipmentConstants.WeaponBow] = new PoolItemOption() { QuantityMin = 1, QuantityMax = 1 },
    };

    public override IEnumerable<Equipment> Claim(Reward reward)
    {
      for (int i = 0; i < Count; i++)
      {
        var item = ItemOptions.GetRandom();
        var equipment = equipmentGenerator.GenerateEquipment(item.Key, reward.Level ?? 1, Rarity, reward.UserId);

        yield return equipment;
      }
    }
  }
}
