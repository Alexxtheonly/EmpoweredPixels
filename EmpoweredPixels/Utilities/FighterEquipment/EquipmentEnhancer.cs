using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Exceptions.Enhancement;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.EquipmentGeneration;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public class EquipmentEnhancer : IEquipmentEnhancer
  {
    private readonly IEquipmentGenerator equipmentGenerator;

    public EquipmentEnhancer(IEquipmentGenerator equipmentGenerator)
    {
      this.equipmentGenerator = equipmentGenerator;
    }

    public int RequiredParticles => 2;

    public void Enhance(IEnhancable enhancable, IEnumerable<Item> particles)
    {
      return;
    }

    public void Enhance(IEnhancable enhancable, IEnumerable<Item> particles, int desiredEnhancement)
    {
      if (desiredEnhancement <= enhancable.Enhancement)
      {
        return;
      }

      int cost = GetCost(enhancable, desiredEnhancement);
      if (particles.Where(o => o.ItemId == EmpoweredParticle.Id).Count() != cost)
      {
        throw new InsufficientEmpoweredParticlesException();
      }

      enhancable.Enhancement = desiredEnhancement;

      if (enhancable.GetType() == typeof(Equipment))
      {
        equipmentGenerator.AdjustStats((Equipment)enhancable);
      }
    }

    public int GetCost(IEnhancable enhancable, int desiredEnhancement)
    {
      if (desiredEnhancement <= enhancable.Enhancement)
      {
        return 0;
      }

      int gausssum = (desiredEnhancement - enhancable.Enhancement).GaussSum();
      return gausssum * RequiredParticles;
    }
  }
}
