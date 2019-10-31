using System;
using EmpoweredPixels.Interfaces.Equipment;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Utilities.EnhancementCalculation
{
  public class EnhancementProbability : IEnhancementProbability
  {
    private const float ChancePerLevel = 10;
    private const float MaxFailChance = 50;

    public bool IsSuccess(IEnhancable enhancable)
    {
      if (enhancable == null)
      {
        throw new ArgumentNullException(nameof(enhancable));
      }

      return !GetFailChance(enhancable.Enhancement).Chance();
    }

    private float GetFailChance(int powerup)
    {
      var chance = powerup * ChancePerLevel;
      return chance > MaxFailChance ? MaxFailChance : chance;
    }
  }
}
