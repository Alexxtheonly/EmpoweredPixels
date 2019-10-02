using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmpoweredPixels.Test.Experience
{
  public class FighterLevelProgressionTest
  {


    private long CalculateExperienceNeeded(int level)
    {
      var factor = 2.5 * (level / 8D);

      const int experienceNeeded = 5120;

      return (long)(experienceNeeded * Math.Pow(factor, 2));
    }
  }
}
