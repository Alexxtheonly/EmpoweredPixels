using System;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.EquipmentGeneration;
using EmpoweredPixels.Utilities.FighterEquipment;
using EmpoweredPixels.Utilities.FighterProgress;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public class SeasonFighterResetter : ISeasonFighterResetter
  {
    private readonly IFighterLevelUpHandler fighterLevelUpHandler;
    private readonly IFighterOutfitter fighterOutfitter;
    private readonly IEquipmentGenerator equipmentGenerator;

    public SeasonFighterResetter(
      IFighterLevelUpHandler fighterLevelUpHandler,
      IFighterOutfitter fighterOutfitter,
      IEquipmentGenerator equipmentGenerator)
    {
      this.fighterLevelUpHandler = fighterLevelUpHandler;
      this.fighterOutfitter = fighterOutfitter;
      this.equipmentGenerator = equipmentGenerator;
    }

    public async Task ResetAsync(DatabaseContext context, long userId)
    {
      var fighters = await context.Fighters
        .Where(o => o.UserId == userId)
        .Include(o => o.Equipment)
        .Include(o => o.Configuration)
        .AsTracking()
        .ToListAsync();

      var random = new Random(Guid.NewGuid().GetHashCode());

      foreach (var fighter in fighters)
      {
        ResetFighter(fighter, random, context);
      }
    }

    private void ResetFighter(Fighter fighter, Random random, DatabaseContext context)
    {
      fighter.Level = 0;

      if (fighter.Configuration != null)
      {
        fighter.Configuration.AttunementId = null;
      }

      fighterLevelUpHandler.Up(fighter);

      var armorHead = equipmentGenerator.GenerateArmorHead(fighter.Level, ItemRarity.Basic, fighter.UserId);
      var armorShoulders = equipmentGenerator.GenerateArmorShoulders(fighter.Level, ItemRarity.Basic, fighter.UserId);
      var armorChest = equipmentGenerator.GenerateArmorChest(fighter.Level, ItemRarity.Basic, fighter.UserId);
      var armorHands = equipmentGenerator.GenerateArmorHands(fighter.Level, ItemRarity.Basic, fighter.UserId);
      var armorLegs = equipmentGenerator.GenerateArmorLegs(fighter.Level, ItemRarity.Basic, fighter.UserId);
      var armorShoes = equipmentGenerator.GenerateArmorShoes(fighter.Level, ItemRarity.Basic, fighter.UserId);

      var weaponType = EquipmentConstants.Weapons.Random(random);
      var weapon = equipmentGenerator.GenerateEquipment(weaponType, fighter.Level, ItemRarity.Basic, fighter.UserId);

      fighterOutfitter.Equip(fighter, armorHead, true);
      fighterOutfitter.Equip(fighter, armorShoulders, true);
      fighterOutfitter.Equip(fighter, armorChest, true);
      fighterOutfitter.Equip(fighter, armorHands, true);
      fighterOutfitter.Equip(fighter, armorLegs, true);
      fighterOutfitter.Equip(fighter, armorShoes, true);

      fighterOutfitter.Equip(fighter, weapon, true);

      context.AddRange(armorHead, armorShoulders, armorChest, armorHands, armorLegs, armorShoes, weapon);
    }
  }
}
