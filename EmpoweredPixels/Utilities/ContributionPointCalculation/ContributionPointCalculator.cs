using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.ContributionPointCalculation
{
  public class ContributionPointCalculator : IContributionPointCalculator
  {
    private const int PointsFirstPlace = 20;
    private const int PointsSecondPlace = 15;
    private const int PointsThirdPlace = 10;

    private const int KillAssistPoints = 20;
    private const int PercentageOfRoundsAlivePoints = 75;
    private const int MatchParticipationPoints = 75;

    public int Calculate(FighterContribution contribution)
    {
      double points = 0;
      if (contribution.HasWon)
      {
        points += PointsFirstPlace;
      }

      if (contribution.IsSecond)
      {
        points += PointsSecondPlace;
      }

      if (contribution.IsThird)
      {
        points += PointsThirdPlace;
      }

      points += contribution.KillsAndAssists * KillAssistPoints;
      points += PercentageOfRoundsAlivePoints * contribution.PercentageOfRoundsAlive;
      points += MatchParticipationPoints * contribution.MatchParticipation;

      return (int)points;
    }
  }
}
