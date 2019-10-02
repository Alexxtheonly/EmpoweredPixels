using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Exceptions.Enhancement;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.EnhancementCalculation;
using EmpoweredPixels.Utilities.EquipmentGeneration;

namespace EmpoweredPixels.Utilities.FighterEquipment
{
  public class EquipmentEnhancer : IEquipmentEnhancer
  {
    private const int ParticlesCost = 5;
    private readonly IEnhancementProbability enhancementProbability;
    private readonly IEquipmentGenerator equipmentGenerator;

    public EquipmentEnhancer(IEnhancementProbability enhancementProbability, IEquipmentGenerator equipmentGenerator)
    {
      this.enhancementProbability = enhancementProbability;
      this.equipmentGenerator = equipmentGenerator;
    }

    public int RequiredParticles => ParticlesCost;

    public void Enhance(IEnhancable enhancable, IEnumerable<Item> particles)
    {
      if (particles.Where(o => o.ItemId == EmpoweredParticle.Id).Count() != ParticlesCost)
      {
        throw new InsufficientEmpoweredParticlesException();
      }

      var success = enhancementProbability.IsSuccess(enhancable);
      if (success)
      {
        enhancable.Enhancement += 1;
      }
      else
      {
        enhancable.Enhancement = 0;
      }

      if (enhancable.GetType() == typeof(Equipment))
      {
        equipmentGenerator.AdjustStats((Equipment)enhancable);
      }
    }
  }
}
