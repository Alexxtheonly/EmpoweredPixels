using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Ratings;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.EloCalculation;
using Microsoft.EntityFrameworkCore;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchFighterEloProcessor : IMatchFighterEloProcessor
  {
    private readonly DatabaseContext databaseContext;
    private readonly IContributionPointCalculator contributionPointCalculator;
    private readonly IEloCalculator eloCalculator;
    private readonly IDateTimeProvider dateTimeProvider;

    public MatchFighterEloProcessor(
      DatabaseContext databaseContext,
      IContributionPointCalculator contributionPointCalculator,
      IEloCalculator eloCalculator,
      IDateTimeProvider dateTimeProvider)
    {
      this.databaseContext = databaseContext;
      this.contributionPointCalculator = contributionPointCalculator;
      this.eloCalculator = eloCalculator;
      this.dateTimeProvider = dateTimeProvider;
    }

    public async Task Process(Match match, IEnumerable<FighterContribution> contributions)
    {
      var positions = new List<EloPosition>();
      var ratings = new List<IEloRating>();

      foreach (var contribution in contributions)
      {
        if (!match.Registrations.Any(o => o.FighterId == contribution.FighterId))
        {
          // do not elo ratings of bots or other non database fighters
          continue;
        }

        positions.Add(new EloPosition()
        {
          Id = contribution.FighterId,
          Points = contributionPointCalculator.Calculate(contribution),
        });

        var eloRating = await databaseContext.FighterEloRatings
          .AsTracking()
          .FirstOrDefaultAsync(o => o.FighterId == contribution.FighterId);

        if (eloRating == null)
        {
          eloRating = new FighterEloRating()
          {
            FighterId = contribution.FighterId,
            CurrentElo = 1500,
            PreviousElo = 1500,
          };
          databaseContext.FighterEloRatings.Add(eloRating);
        }

        eloRating.LastUpdate = dateTimeProvider.Now;

        ratings.Add(eloRating);
      }

      eloCalculator.Calculate(ratings, positions);

      await databaseContext.SaveChangesAsync();
    }
  }
}
