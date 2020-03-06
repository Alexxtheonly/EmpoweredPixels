using System.Collections.Generic;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Items;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public interface IEquipmentEnhancer
  {
    int RequiredParticles { get; }

    void Enhance(IEnhancable enhancable, IEnumerable<Item> particles);

    void Enhance(IEnhancable enhancable, IEnumerable<Item> particles, int desiredEnhancement);

    int GetCost(IEnhancable enhancable, int desiredEnhancement);
  }
}
