using System;
using EmpoweredPixels.Enums.Equipment;

namespace EmpoweredPixels.Rewards.Items
{
  public class EquipmentToken : RewardItemBase
  {
    public static readonly Guid Common = new Guid("6FE9907B-A4D9-4A45-A8C6-29BAE0D6A5A6");

    public static readonly Guid Rare = new Guid("BD973A9C-E294-4489-8841-2AD892F8F2F8");

    public static readonly Guid Fabled = new Guid("FD3BE114-08AB-45AD-B5D5-23D7A1906206");

    public static readonly Guid Mythic = new Guid("B583E208-3290-4660-83C6-67C151212261");

    public static readonly Guid Legendary = new Guid("2DC028CD-16B5-47DC-A192-BCF0D35B4D1A");

    public static Guid Get(ItemRarity rarity)
    {
      switch (rarity)
      {
        case ItemRarity.Common:
          return Common;
        case ItemRarity.Rare:
          return Rare;
        case ItemRarity.Fabled:
          return Fabled;
        case ItemRarity.Mythic:
          return Mythic;
        case ItemRarity.Legendary:
          return Legendary;
        case ItemRarity.Basic:
        default:
          throw new Exception("Invalid rarity for equipment token");
      }
    }
  }
}
