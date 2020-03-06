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
    private readonly RandomEquipmentEnhancer randomEquipmentEnhancer = new RandomEquipmentEnhancer(new EnhancementProbability(), new EquipmentGenerator());

    private readonly EquipmentEnhancer equipmentEnhancer = new EquipmentEnhancer(new EquipmentGenerator());

    [Fact]
    public void ShouldEnhance()
    {
      var enhanceable = Mock.Of<IEnhancable>();

      var maxEnhancement = 0;

      for (int i = 0; i < 1000000; i++)
      {
        randomEquipmentEnhancer.Enhance(enhanceable, GetParticles(randomEquipmentEnhancer.RequiredParticles));
        if (enhanceable.Enhancement > maxEnhancement)
        {
          maxEnhancement = enhanceable.Enhancement;
        }
      }
    }

    [Theory]
    [InlineData(0, 5)]
    [InlineData(0, 15)]
    [InlineData(0, 50)]
    public void ShouldEnhanceDesired(int currentEnhancement, int desiredEnhancement)
    {
      var enhancable = Mock.Of<IEnhancable>();
      enhancable.Enhancement = currentEnhancement;

      var cost = equipmentEnhancer.GetCost(enhancable, desiredEnhancement);
      equipmentEnhancer.Enhance(enhancable, GetParticles(cost), desiredEnhancement);

      Assert.Equal(desiredEnhancement, enhancable.Enhancement);
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
