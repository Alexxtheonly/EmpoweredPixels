using System;
using System.Collections.Generic;
using System.Linq;

namespace EmpoweredPixels.Utilities.EloCalculation
{
  public class EloCalculator : IEloCalculator
  {
    public void Calculate(IEnumerable<IEloRating> eloRatings, IEnumerable<EloPosition> eloPositions)
    {
      var ratingsDictionary = GetDictionary(eloRatings);
      var positionsOrdered = eloPositions
        .OrderByDescending(o => o.Points)
        .ToList();

      var count = positionsOrdered.Count();
      UpdatePreviousRating(eloRatings);

      for (int i = 0; i < count; i++)
      {
        bool hasNext = i < count - 1;

        var current = ratingsDictionary[positionsOrdered.ElementAt(i).Id];

        if (hasNext)
        {
          var next = ratingsDictionary[positionsOrdered.ElementAt(i + 1).Id];

          CalculateElo(current, next);
        }
      }
    }

    private void UpdatePreviousRating(IEnumerable<IEloRating> eloRatings)
    {
      foreach (var eloRating in eloRatings)
      {
        eloRating.PreviousElo = eloRating.CurrentElo;
      }
    }

    private void CalculateElo(IEloRating winner, IEloRating loser)
    {
      var result = CalculateElo(winner.CurrentElo, loser.CurrentElo, Outcome.Win);

      winner.CurrentElo = result.NewRatingPlayerLeft;
      loser.CurrentElo = result.NewRatingPlayerRight;
    }

    private EloResult CalculateElo(int playerLeftRating, int playerRightRating, Outcome outcome)
    {
      // Number between 10 = slow change and 32 = fast change
      const int eloK = 16;

      int delta = (int)(eloK * ((int)outcome - ExpectationToWin(playerLeftRating, playerRightRating)));

      return new EloResult()
      {
        OldRatingPlayerLeft = playerLeftRating,
        OldRatingPlayerRight = playerRightRating,
        NewRatingPlayerLeft = playerLeftRating + delta,
        NewRatingPlayerRight = playerRightRating - delta,
      };
    }

    private double ExpectationToWin(int playerLeftRating, int playerRightRating)
    {
      return 1 / (1 + Math.Pow(10, (playerRightRating - playerLeftRating) / 400D));
    }

    private Dictionary<Guid, IEloRating> GetDictionary(IEnumerable<IEloRating> eloRatings)
    {
      return new Dictionary<Guid, IEloRating>(eloRatings.Select(o => new KeyValuePair<Guid, IEloRating>(o.FighterId, o)));
    }
  }
}
