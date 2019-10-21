using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Models.Leagues;

namespace EmpoweredPixels.Utilities.LeageExecution
{
  public class LeagueDivisionDivider : ILeagueDivisionDivider
  {
    private const int MinSize = 4;
    private const int MaxSize = 25;

    private const int EloDifferenceSplit = 50;

    public LeagueDivisionDivider()
    {
    }

    public IEnumerable<IGrouping<int, LeagueSubscription>> GetDivisions(IEnumerable<LeagueSubscription> subscriptions)
    {
      var sortedByPowerlevel = subscriptions
        .Select(o => new { Subscription = o, Elo = o.Fighter.EloRating?.CurrentElo ?? 0 })
        .OrderBy(o => o.Elo)
        .ToList();

      var groups = new List<Group<int, LeagueSubscription>>();
      var group = new Group<int, LeagueSubscription>();
      int? minEloInGroup = null;

      for (int i = 0; i < sortedByPowerlevel.Count; i++)
      {
        var sub = sortedByPowerlevel[i];

        if (minEloInGroup == null)
        {
          minEloInGroup = sub.Elo;
        }

        bool eloDifferenceTooBig = (sub.Elo - minEloInGroup) > EloDifferenceSplit;
        bool canBeDivided = (group.Count >= MinSize && eloDifferenceTooBig) || group.Count >= MaxSize;
        bool remainingAreValidDivisionSize = (sortedByPowerlevel.Count - i) >= MinSize;

        if (canBeDivided && remainingAreValidDivisionSize)
        {
          groups.Add(group);

          group = new Group<int, LeagueSubscription>();
          minEloInGroup = null;
        }

        group.Add(sub.Subscription);
      }

      groups.Add(group);

      var divisionNumber = groups.Count;
      foreach (var division in groups)
      {
        division.Key = divisionNumber--;
      }

      return groups;
    }

    private class Group<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
      public TKey Key { get; set; }
    }
  }
}
