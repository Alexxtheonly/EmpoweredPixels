using System;
using System.Linq;
using EmpoweredPixels.Exceptions.Roster;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public class FighterOutfitter : IFighterOutfitter
  {
    public bool IsValidOutfit(Fighter fighter)
    {
      if (fighter.Equipment == null)
      {
        throw new ArgumentNullException(nameof(Fighter.Equipment));
      }

      var headCount = fighter.Equipment.Count(o => o.Type == EquipmentConstants.ArmorHead);
      var shouldersCount = fighter.Equipment.Count(o => o.Type == EquipmentConstants.ArmorShoulders);
      var chestCount = fighter.Equipment.Count(o => o.Type == EquipmentConstants.ArmorChest);
      var handsCount = fighter.Equipment.Count(o => o.Type == EquipmentConstants.ArmorHands);
      var legsCount = fighter.Equipment.Count(o => o.Type == EquipmentConstants.ArmorLegs);
      var shoesCount = fighter.Equipment.Count(o => o.Type == EquipmentConstants.ArmorShoes);
      var weaponCount = fighter.Equipment.Count(o => EquipmentConstants.IsWeaponConstant(o.Type));

      var counts = new int[]
      {
        headCount,
        shouldersCount,
        chestCount,
        handsCount,
        legsCount,
        shoesCount,
        weaponCount,
      };

      return !counts.Any(o => o > 1);
    }

    public void Equip(Fighter fighter, Equipment equipment, bool unequipExisting)
    {
      if (fighter.Equipment == null)
      {
        throw new ArgumentNullException(nameof(Fighter.Equipment));
      }

      bool isWeapon = EquipmentConstants.IsWeaponConstant(equipment.Type);

      var existing = fighter.Equipment.FirstOrDefault(o => o.Type == equipment.Type || (isWeapon && EquipmentConstants.IsWeaponConstant(o.Type)));
      if (existing != null && !unequipExisting)
      {
        throw new InvalidEquipmentOperationException();
      }

      if (fighter.Level < equipment.Level)
      {
        throw new InvalidEquipmentOperationException();
      }

      if (existing != null)
      {
        existing.FighterId = null;
      }

      equipment.FighterId = fighter.Id;
      equipment.Fighter = fighter;
    }

    public void Unequip(Fighter fighter, Equipment equipment)
    {
      if (fighter.Equipment == null)
      {
        throw new ArgumentNullException(nameof(Fighter.Equipment));
      }

      var existing = fighter.Equipment.FirstOrDefault(o => o.Id == equipment.Id);
      if (existing == null)
      {
        throw new ArgumentNullException(nameof(existing));
      }

      existing.FighterId = null;
    }
  }
}
