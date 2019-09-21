using System.Collections.Generic;

namespace EmpoweredPixels.Utilities.EloCalculation
{
  public interface IEloCalculator
  {
    void Calculate(IEnumerable<IEloRating> eloRatings, IEnumerable<EloPosition> eloPositions);
  }
}
