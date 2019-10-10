using System;
using System.Collections.Generic;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.EquipmentGeneration;
using EmpoweredPixels.Utilities.FighterProgress;
using EmpoweredPixels.Utilities.FighterSkillSelection;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using Moq;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Test.Balance
{
  public class TestFighterFactory
  {
    private readonly EquipmentGenerator equipmentGenerator = new EquipmentGenerator();
    private readonly FighterStatCalculator fighterStatCalculator = new FighterStatCalculator();
    private readonly FighterLevelUpHandler fighterLevelUpHandler = new FighterLevelUpHandler(Mock.Of<IDateTimeProvider>());
    private readonly FighterSkillSelector fighterSkillSelector = new FighterSkillSelector();

    public IFighterStats GetBowFighter(int level, ItemRarity itemRarity)
    {
      var fighter = GetFighterWithArmor(level, itemRarity);
      SetFighterBow(fighter, level, itemRarity);

      return GetEngineFighter(fighter);
    }

    public IFighterStats GetGreatswordFighter(int level, ItemRarity itemRarity)
    {
      var fighter = GetFighterWithArmor(level, itemRarity);
      SetFighterGreatsword(fighter, level, itemRarity);

      return GetEngineFighter(fighter);
    }

    public IFighterStats GetDaggerFighter(int level, ItemRarity itemRarity)
    {
      var fighter = GetFighterWithArmor(level, itemRarity);
      SetFighterDagger(fighter, level, itemRarity);

      return GetEngineFighter(fighter);
    }

    public IFighterStats GetGaiveFighter(int level, ItemRarity itemRarity)
    {
      var fighter = GetFighterWithArmor(level, itemRarity);
      SetFighterGlaive(fighter, level, itemRarity);

      return GetEngineFighter(fighter);
    }

    public IFighterStats GetEngineFighter(Fighter fighter)
    {
      return new AdvancedFighter()
      {
        Id = fighter.Id,
        Team = null,
        Stats = fighterStatCalculator.Calculate(fighter),
        Skills = fighterSkillSelector.GetSkills(fighter),
      };
    }

    public void SetFighterDagger(Fighter fighter, int level, ItemRarity itemRarity)
    {
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateWeaponDagger(GetEquipmentLevel(level), itemRarity, 0)));
    }

    public void SetFighterGreatsword(Fighter fighter, int level, ItemRarity itemRarity)
    {
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateWeaponGreatsword(GetEquipmentLevel(level), itemRarity, 0)));
    }

    public void SetFighterBow(Fighter fighter, int level, ItemRarity itemRarity)
    {
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateWeaponBow(GetEquipmentLevel(level), itemRarity, 0)));
    }

    public void SetFighterGlaive(Fighter fighter, int level, ItemRarity itemRarity)
    {
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateWeaponGlaive(GetEquipmentLevel(level), itemRarity, 0)));
    }

    public Fighter GetFighter(int level)
    {
      var fighter = new Fighter()
      {
        Id = Guid.NewGuid(),
        Equipment = new List<Equipment>(),
      };

      for (int i = 0; i < level; i++)
      {
        fighterLevelUpHandler.Up(fighter);
      }

      return fighter;
    }

    public void SetFighterEquipment(Fighter fighter, int level, ItemRarity itemRarity)
    {
      var equipmentLevel = GetEquipmentLevel(level);

      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateArmorHead(equipmentLevel, itemRarity, 0)));
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateArmorShoulders(equipmentLevel, itemRarity, 0)));
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateArmorChest(equipmentLevel, itemRarity, 0)));
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateArmorHands(equipmentLevel, itemRarity, 0)));
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateArmorLegs(equipmentLevel, itemRarity, 0)));
      fighter.Equipment.Add(AddSocket(equipmentGenerator.GenerateArmorShoes(equipmentLevel, itemRarity, 0)));
    }

    private Equipment AddSocket(Equipment equipment)
    {
      equipment.SocketStones = new List<SocketStone>();
      return equipment;
    }

    private Fighter GetFighterWithArmor(int level, ItemRarity itemRarity)
    {
      var fighter = GetFighter(level);
      SetFighterEquipment(fighter, level, itemRarity);
      return fighter;
    }

    private int GetEquipmentLevel(int level)
    {
      if (level == 1)
      {
        return 1;
      }

      return level.NearestBase(8);
    }
  }
}
