using System;
using System.Collections.Generic;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Utilities.EquipmentGeneration
{
  public class EquipmentGenerator : IEquipmentGenerator
  {
    private const int BaseValue = 10;

    private static readonly Dictionary<Guid, Action<Equipment>> StatAdjustments = new Dictionary<Guid, Action<Equipment>>()
    {
      [EquipmentConstants.ArmorHead] = o => AdjustArmorHeadStats(o),
      [EquipmentConstants.ArmorShoulders] = o => AdjustArmorShouldersStats(o),
      [EquipmentConstants.ArmorChest] = o => AdjustArmorChestStats(o),
      [EquipmentConstants.ArmorHands] = o => AdjustArmorHandsStats(o),
      [EquipmentConstants.ArmorLegs] = o => AdjustArmorLegsStats(o),
      [EquipmentConstants.ArmorShoes] = o => AdjustArmorShoesStats(o),
      [EquipmentConstants.WeaponGreatsword] = o => AdjustWeaponGreatswordStats(o),
      [EquipmentConstants.WeaponBow] = o => AdjustWeaponBowStats(o),
      [EquipmentConstants.WeaponDagger] = o => AdjustWeaponDaggerStats(o),
      [EquipmentConstants.WeaponGlaive] = o => AdjustWeaponGlaiveStats(o),
    };

    public Equipment GenerateArmorHead(int level, ItemRarity rarity, long userId)
    {
      var armorHead = Generate(EquipmentConstants.ArmorHead, level, rarity, userId);

      AdjustArmorHeadStats(armorHead);

      return armorHead;
    }

    public Equipment GenerateArmorShoulders(int level, ItemRarity rarity, long userId)
    {
      var armorShoulders = Generate(EquipmentConstants.ArmorShoulders, level, rarity, userId);

      AdjustArmorShouldersStats(armorShoulders);

      return armorShoulders;
    }

    public Equipment GenerateArmorChest(int level, ItemRarity rarity, long userId)
    {
      var armorChest = Generate(EquipmentConstants.ArmorChest, level, rarity, userId);

      AdjustArmorChestStats(armorChest);

      return armorChest;
    }

    public Equipment GenerateArmorHands(int level, ItemRarity rarity, long userId)
    {
      var armorHands = Generate(EquipmentConstants.ArmorHands, level, rarity, userId);

      AdjustArmorHandsStats(armorHands);

      return armorHands;
    }

    public Equipment GenerateArmorLegs(int level, ItemRarity rarity, long userId)
    {
      var armorLegs = Generate(EquipmentConstants.ArmorLegs, level, rarity, userId);

      AdjustArmorLegsStats(armorLegs);

      return armorLegs;
    }

    public Equipment GenerateArmorShoes(int level, ItemRarity rarity, long userId)
    {
      var armorShoes = Generate(EquipmentConstants.ArmorShoes, level, rarity, userId);

      AdjustArmorShoesStats(armorShoes);

      return armorShoes;
    }

    public Equipment GenerateWeaponGreatsword(int level, ItemRarity rarity, long userId)
    {
      var weaponGreatsword = Generate(EquipmentConstants.WeaponGreatsword, level, rarity, userId);

      AdjustWeaponGreatswordStats(weaponGreatsword);

      return weaponGreatsword;
    }

    public Equipment GenerateWeaponBow(int level, ItemRarity rarity, long userId)
    {
      var weaponBow = Generate(EquipmentConstants.WeaponBow, level, rarity, userId);

      AdjustWeaponBowStats(weaponBow);

      return weaponBow;
    }

    public Equipment GenerateWeaponDagger(int level, ItemRarity rarity, long userId)
    {
      var weaponDagger = Generate(EquipmentConstants.WeaponDagger, level, rarity, userId);

      AdjustWeaponDaggerStats(weaponDagger);

      return weaponDagger;
    }

    public Equipment GenerateWeaponGlaive(int level, ItemRarity rarity, long userId)
    {
      var weaponGlaive = Generate(EquipmentConstants.WeaponGlaive, level, rarity, userId);

      AdjustWeaponGlaiveStats(weaponGlaive);

      return weaponGlaive;
    }

    public void AdjustStats(Equipment equipment)
    {
      StatAdjustments[equipment.Type].Invoke(equipment);
    }

    public Equipment GenerateEquipment(Guid type, int level, ItemRarity rarity, long userId)
    {
      var equipment = Generate(type, level, rarity, userId);

      AdjustStats(equipment);

      return equipment;
    }

    private static void AdjustArmorHeadStats(Equipment armorHead)
    {
      if (armorHead.Type != EquipmentConstants.ArmorHead)
      {
        throw new ArgumentException("Armor head is no armor head");
      }

      AdjustArmorStat(armorHead, 0.2F);
    }

    private static void AdjustArmorShouldersStats(Equipment armorShoulders)
    {
      if (armorShoulders.Type != EquipmentConstants.ArmorShoulders)
      {
        throw new ArgumentException("Armor shoulders is no armor shoulders");
      }

      AdjustArmorStat(armorShoulders, 0.1F);
    }

    private static void AdjustArmorChestStats(Equipment armorChest)
    {
      if (armorChest.Type != EquipmentConstants.ArmorChest)
      {
        throw new ArgumentException("Armor chest is no armor chest");
      }

      AdjustArmorStat(armorChest, 0.3F);
    }

    private static void AdjustArmorHandsStats(Equipment armorHands)
    {
      if (armorHands.Type != EquipmentConstants.ArmorHands)
      {
        throw new ArgumentException("Armor hands is no armor hands");
      }

      AdjustArmorStat(armorHands, 0.1F);
    }

    private static void AdjustArmorLegsStats(Equipment armorLegs)
    {
      if (armorLegs.Type != EquipmentConstants.ArmorLegs)
      {
        throw new ArgumentException("Armor legs is no armor legs");
      }

      AdjustArmorStat(armorLegs, 0.2F);
    }

    private static void AdjustArmorShoesStats(Equipment armorShoes)
    {
      if (armorShoes.Type != EquipmentConstants.ArmorShoes)
      {
        throw new ArgumentException("Armor shoes is no armor shoes");
      }

      AdjustArmorStat(armorShoes, 0.1F);
    }

    private static void AdjustWeaponGreatswordStats(Equipment weaponGreatsword)
    {
      if (weaponGreatsword.Type != EquipmentConstants.WeaponGreatsword)
      {
        throw new ArgumentException("Weapon greatsword is no weapon greatsword");
      }

      AdjustWeaponStatPower(weaponGreatsword, 1);
      AdjustWeaponStatParryChance(weaponGreatsword, 15);
    }

    private static void AdjustWeaponBowStats(Equipment weaponBow)
    {
      if (weaponBow.Type != EquipmentConstants.WeaponBow)
      {
        throw new ArgumentException("Weapon bow is no weapon bow");
      }

      AdjustWeaponStatPower(weaponBow, 1);
    }

    private static void AdjustWeaponDaggerStats(Equipment weaponDagger)
    {
      if (weaponDagger.Type != EquipmentConstants.WeaponDagger)
      {
        throw new ArgumentException("Weapon dagger is no weapon dagger");
      }

      AdjustWeaponStatConditionPower(weaponDagger, 0.5F);
      AdjustWeaponStatPower(weaponDagger, 0.5F);
      AdjustWeaponStatParryChance(weaponDagger, 5);
    }

    private static void AdjustWeaponGlaiveStats(Equipment weaponGlaive)
    {
      if (weaponGlaive.Type != EquipmentConstants.WeaponGlaive)
      {
        throw new ArgumentException("Weapon glaive is no weapon glaive");
      }

      AdjustWeaponStatPower(weaponGlaive, 1);
      AdjustWeaponStatParryChance(weaponGlaive, 15);
    }

    private static void AdjustArmorStat(Equipment armor, float factor)
    {
      armor.Armor = CalculateStat(armor, factor);
    }

    private static void AdjustWeaponStatPower(Equipment weapon, float factor)
    {
      weapon.Power = CalculateStat(weapon, factor);
    }

    private static void AdjustWeaponStatConditionPower(Equipment weapon, float factor)
    {
      weapon.ConditionPower = CalculateStat(weapon, factor);
    }

    private static void AdjustWeaponStatParryChance(Equipment weapon, int value)
    {
      weapon.ParryChance = value;
    }

    private static int CalculateStat(Equipment equipment, float factor)
    {
      return (int)((equipment.Level + (int)equipment.Rarity + equipment.Enhancement) * factor * BaseValue);
    }

    private Equipment Generate(Guid type, int level, ItemRarity rarity, long userId)
    {
      return new Equipment()
      {
        Type = type,
        Level = level,
        Rarity = rarity,
        UserId = userId,
      };
    }
  }
}
