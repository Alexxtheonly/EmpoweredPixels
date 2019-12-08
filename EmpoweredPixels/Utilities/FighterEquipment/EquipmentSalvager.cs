using System.Collections.Generic;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public class EquipmentSalvager : IEquipmentSalvager
  {
    private const int BaseValue = 30;

    public IEnumerable<Item> Salvage(IEnhancable enhancable, long userId)
    {
      var value = GetSalvageValue(enhancable);

      for (int i = 0; i < value; i++)
      {
        yield return new Item()
        {
          ItemId = EmpoweredParticle.Id,
          Rarity = Enums.Equipment.ItemRarity.Basic,
          UserId = userId,
        };
      }

      if (enhancable.Rarity != Enums.Equipment.ItemRarity.Basic && 20F.Chance())
      {
        yield return new Item()
        {
          ItemId = EquipmentToken.Get(enhancable.Rarity),
          Rarity = enhancable.Rarity,
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

      return (int)enhancable.Rarity * BaseValue;
    }
  }
}
