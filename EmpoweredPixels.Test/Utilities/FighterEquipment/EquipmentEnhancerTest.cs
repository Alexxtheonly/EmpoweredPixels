using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.EnhancementCalculation;
using EmpoweredPixels.Utilities.EquipmentGeneration;
using EmpoweredPixels.Utilities.FighterEquipment;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EmpoweredPixels.Test.Utilities.FighterEquipment
{
  public class EquipmentEnhancerTest
  {
    private readonly EquipmentEnhancer equipmentEnhancer = new EquipmentEnhancer(new EnhancementProbability(), new EquipmentGenerator());


    [Fact]
    public void ShouldEnhance()
    {
      var enhanceable = Mock.Of<IEnhancable>();

      var maxEnhancement = 0;

      for (int i = 0; i < 1000000; i++)
      {
        equipmentEnhancer.Enhance(enhanceable, GetParticles(equipmentEnhancer.RequiredParticles));
        if (enhanceable.Enhancement > maxEnhancement)
        {
          maxEnhancement = enhanceable.Enhancement;
        }
      }
    }

    private IEnumerable<Item> GetParticles(int count)
    {
      for (int i = 0; i < count; i++)
      {
        yield return new Item()
        {
          Id = Guid.NewGuid(),
          ItemId = EmpoweredParticle.Id,
        };
      }
    }
  }
}
