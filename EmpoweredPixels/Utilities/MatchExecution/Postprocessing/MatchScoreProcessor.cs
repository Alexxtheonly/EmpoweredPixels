using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchScoreProcessor : IMatchScoreProcessor
  {
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly DatabaseContext databaseContext;

    public MatchScoreProcessor(IDateTimeProvider dateTimeProvider, DatabaseContext databaseContext)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.databaseContext = databaseContext;
    }

    public async Task Process(Match match, IEnumerable<FighterMatchScore> scores)
    {
      foreach (var fighterScore in scores)
      {
        if (!match.Registrations.Any(o => o.FighterId == fighterScore.Id))
        {
          // do not save scores of bots or other non database fighters
          continue;
        }

        databaseContext.MatchScoreFighters.Add(new MatchScoreFighter()
        {
          Created = dateTimeProvider.Now,
          FighterId = fighterScore.Id,
          MatchId = match.Id,
          RoundsAlive = fighterScore.RoundsAlive,
          TotalDamageDone = fighterScore.TotalDamageDone,
          TotalDamageTaken = fighterScore.TotalDamageTaken,
          TotalDeaths = fighterScore.TotalDeaths,
          TotalDistanceTraveled = Math.Round(fighterScore.TotalDistanceTraveled, 2),
          TotalKills = fighterScore.TotalKills,
          TotalAssists = fighterScore.TotalAssists,
        });
      }

      await databaseContext.SaveChangesAsync();
    }
  }
}
