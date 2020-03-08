using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchContributionProcessor : IMatchContributionProcessor
  {
    private readonly DatabaseContext databaseContext;

    public MatchContributionProcessor(DatabaseContext databaseContext)
    {
      this.databaseContext = databaseContext;
    }

    public async Task Process(Match match, IEnumerable<FighterContribution> contributions)
    {
      foreach (var contribution in contributions)
      {
        if (!match.Registrations.Any(o => o.FighterId == contribution.FighterId))
        {
          // do not save contributions of bots or other non database fighters
          continue;
        }

        databaseContext.MatchContributions.Add(new MatchContribution()
        {
          FighterId = contribution.FighterId,
          HasWon = contribution.HasWon,
          IsSecond = contribution.IsSecond,
          IsThird = contribution.IsThird,
          Kills = contribution.Kills,
          Assists = contribution.Assists,
          MatchId = match.Id,
          MatchParticipation = contribution.MatchParticipation,
          PercentageOfRoundsAlive = contribution.PercentageOfRoundsAlive,
        });
      }

      await databaseContext.SaveChangesAsync();
    }
  }
}
