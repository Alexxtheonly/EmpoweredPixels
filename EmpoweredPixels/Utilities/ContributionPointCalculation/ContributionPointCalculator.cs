using System.Linq;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.ContributionPointCalculation
{
  public class ContributionPointCalculator : IContributionPointCalculator
  {
    private const int PointsFirstPlace = 100;
    private const int PointsSecondPlace = 75;
    private const int PointsThirdPlace = 50;

    private const int KillPoints = 20;
    private const int AssistPoints = 5;
    private const int PercentageOfRoundsAlivePoints = 10;
    private const int MatchParticipationPoints = 10;

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

      points += contribution.Kills.Count() * KillPoints;
      points += contribution.Assists.Count() * AssistPoints;
      points += PercentageOfRoundsAlivePoints * contribution.PercentageOfRoundsAlive;
      points += MatchParticipationPoints * contribution.MatchParticipation;

      return (int)points;
    }
  }
}
