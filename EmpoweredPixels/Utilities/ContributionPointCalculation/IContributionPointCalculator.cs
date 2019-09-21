using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.ContributionPointCalculation
{
  public interface IContributionPointCalculator
  {
    int Calculate(FighterContribution contribution);
  }
}
