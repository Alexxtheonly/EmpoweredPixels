using System.Collections.Generic;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.RewardTrackCalculation;
using Microsoft.EntityFrameworkCore;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchFighterRewardTrackProcessor : IMatchFighterRewardTrackProcessor
  {
    private readonly DatabaseContext databaseContext;
    private readonly IContributionPointCalculator contributionPointCalculator;
    private readonly IRewardTrackCalculator rewardTrackCalculator;

    public MatchFighterRewardTrackProcessor(
      DatabaseContext databaseContext,
      IContributionPointCalculator contributionPointCalculator,
      IRewardTrackCalculator rewardTrackCalculator)
    {
      this.databaseContext = databaseContext;
      this.contributionPointCalculator = contributionPointCalculator;
      this.rewardTrackCalculator = rewardTrackCalculator;
    }

    public async Task Process(IEnumerable<FighterContribution> contributions)
    {
      foreach (var contribution in contributions)
      {
        var fighter = await databaseContext.Fighters
          .FirstOrDefaultAsync(o => o.Id == contribution.FighterId);

        if (fighter == null)
        {
          continue;
        }

        var points = contributionPointCalculator.Calculate(contribution);
        await rewardTrackCalculator.Calculate(fighter, points);
      }
    }
  }
}
