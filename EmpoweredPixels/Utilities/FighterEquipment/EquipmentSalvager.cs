using System.Collections.Generic;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public class EquipmentSalvager : IEquipmentSalvager
  {
    private const int BaseValue = 5;

    public IEnumerable<Item> Salvage(IEnhancable enhancable, long userId)
    {
      var value = GetSalvageValue(enhancable);

      for (int i = 0; i < value; i++)
      {
        yield return new Item()
        {
          ItemId = EmpoweredParticle.Id,
          UserId = userId,
        };
      }
    }

    private int GetSalvageValue(IEnhancable enhancable)
    {
      if (enhancable.Rarity == Enums.Equipment.ItemRarity.Basic)
      {
        return 0;
      }

      return (enhancable.Level + (int)enhancable.Rarity + enhancable.Enhancement) * BaseValue;
    }
  }
}
