using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.ContributionPointCalculation
{
  public class ContributionPointCalculator : IContributionPointCalculator
  {
    private const int WinPoints = 200;
    private const int KillAssistPoints = 50;
    private const int PercentageOfRoundsAlivePoints = 25;
    private const int MatchParticipationPoints = 15;

    public int Calculate(FighterContribution contribution)
    {
      double points = 0;
      if (contribution.HasWon)
      {
        points += WinPoints;
      }

      points += contribution.KillsAndAssists * KillAssistPoints;
      points += PercentageOfRoundsAlivePoints * contribution.PercentageOfRoundsAlive;
      points += MatchParticipationPoints * contribution.MatchParticipation;

      return (int)points;
    }
  }
}
