using System;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Models.Items;

namespace EmpoweredPixels.Utilities.EquipmentGeneration
{
  public interface IEquipmentGenerator
  {
    void AdjustStats(Equipment equipment);

    Equipment GenerateEquipment(Guid type, int level, ItemRarity rarity, long userId);

    Equipment GenerateArmorChest(int level, ItemRarity rarity, long userId);

    Equipment GenerateArmorHands(int level, ItemRarity rarity, long userId);

    Equipment GenerateArmorHead(int level, ItemRarity rarity, long userId);

    Equipment GenerateArmorLegs(int level, ItemRarity rarity, long userId);

    Equipment GenerateArmorShoes(int level, ItemRarity rarity, long userId);

    Equipment GenerateArmorShoulders(int level, ItemRarity rarity, long userId);

    Equipment GenerateWeaponBow(int level, ItemRarity rarity, long userId);

    Equipment GenerateWeaponGreatsword(int level, ItemRarity rarity, long userId);

    Equipment GenerateWeaponDagger(int level, ItemRarity rarity, long userId);

    Equipment GenerateWeaponGlaive(int level, ItemRarity rarity, long userId);
  }
}
