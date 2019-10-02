using System.Collections.Generic;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Items;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public interface IEquipmentSalvager
  {
    IEnumerable<Item> Salvage(IEnhancable enhancable, long userId);
  }
}
