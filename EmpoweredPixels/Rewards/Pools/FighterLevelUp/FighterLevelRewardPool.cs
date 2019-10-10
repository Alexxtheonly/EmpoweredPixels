using System;
using System.Collections.Generic;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Utilities.EquipmentGeneration;

namespace EmpoweredPixels.Rewards.Pools.FighterLevelUp
{
  public class FighterLevelRewardPool : RewardPoolBase<Equipment>
  {
    public static readonly Guid Id = new Guid("7C9E9FC4-9170-420D-B8D2-42812E8E2EA4");

    private readonly IEquipmentGenerator equipmentGenerator;

    public FighterLevelRewardPool(IEquipmentGenerator equipmentGenerator)
    {
      this.equipmentGenerator = equipmentGenerator;
    }

    protected override Dictionary<Guid, PoolItemOption> ItemOptions => new Dictionary<Guid, PoolItemOption>();

    public override IEnumerable<Equipment> Claim(Reward reward)
    {
      var level = (reward.Level ?? 1).NearestBase(8);

      if (level == 0)
      {
        level = 1;
      }

      var head = equipmentGenerator.GenerateArmorHead(level, ItemRarity.Basic, reward.UserId);
      var shoulders = equipmentGenerator.GenerateArmorShoulders(level, ItemRarity.Basic, reward.UserId);
      var chest = equipmentGenerator.GenerateArmorChest(level, ItemRarity.Basic, reward.UserId);
      var hands = equipmentGenerator.GenerateArmorHands(level, ItemRarity.Basic, reward.UserId);
      var legs = equipmentGenerator.GenerateArmorLegs(level, ItemRarity.Basic, reward.UserId);
      var shoes = equipmentGenerator.GenerateArmorShoes(level, ItemRarity.Basic, reward.UserId);
      var greatsword = equipmentGenerator.GenerateWeaponGreatsword(level, ItemRarity.Basic, reward.UserId);
      var bow = equipmentGenerator.GenerateWeaponBow(level, ItemRarity.Basic, reward.UserId);
      var dagger = equipmentGenerator.GenerateWeaponDagger(level, ItemRarity.Basic, reward.UserId);
      var glaive = equipmentGenerator.GenerateWeaponGlaive(level, ItemRarity.Basic, reward.UserId);

      return new Equipment[]
      {
        head,
        shoulders,
        chest,
        hands,
        legs,
        shoes,
        greatsword,
        bow,
        dagger,
        glaive,
      };
    }
  }
}
