using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmpoweredPixels.Test.Experience
{
  public class FighterLevelProgressionTest
  {
    [Theory]
    [InlineData(1, 500)]
    [InlineData(8, 32000)]
    [InlineData(16, 128000)]
    [InlineData(24, 288000)]
    [InlineData(32, 512000)]
    [InlineData(5, 12500)]
    [InlineData(14, 98000)]
    [InlineData(30, 450000)]
    public void ShouldReturnCorrectFighterExperience(int level, long expected)
    {
      var actual = CalculateExperienceNeeded(level);
      Assert.Equal(expected, actual);
    }

    private long CalculateExperienceNeeded(int level)
    {
      var factor = 2.5 * (level / 8D);

      const int experienceNeeded = 5120;

      return (long)(experienceNeeded * Math.Pow(factor, 2));
    }
  }
}
