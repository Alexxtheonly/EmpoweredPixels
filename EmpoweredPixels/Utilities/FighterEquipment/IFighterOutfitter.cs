using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public interface IFighterOutfitter
  {
    void Equip(Fighter fighter, Equipment equipment, bool unequipExisting);

    bool IsValidOutfit(Fighter fighter);

    void Unequip(Fighter fighter, Equipment equipment);
  }
}
