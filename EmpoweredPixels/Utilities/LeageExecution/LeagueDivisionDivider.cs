using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Utilities.LeageExecution
{
  public class LeagueDivisionDivider : ILeagueDivisionDivider
  {
    private const int MinSize = 3;
    private const int MaxSize = 20;

    private const int PowerlevelDifferenceSplit = 120;

    private readonly IFighterStatCalculator fighterStatCalculator;

    public LeagueDivisionDivider(IFighterStatCalculator fighterStatCalculator)
    {
      this.fighterStatCalculator = fighterStatCalculator;
    }

    public IEnumerable<IGrouping<int, LeagueSubscription>> GetDivisions(IEnumerable<LeagueSubscription> subscriptions)
    {
      var sortedByPowerlevel = subscriptions
        .Select(o => new { Subscription = o, Powerlevel = CalculatePowerlevel(o.Fighter) })
        .OrderBy(o => o.Powerlevel)
        .ToList();

      var groups = new List<Group<int, LeagueSubscription>>();
      var group = new Group<int, LeagueSubscription>();
      int? minPowerlevelInGroup = null;

      for (int i = 0; i < sortedByPowerlevel.Count; i++)
      {
        var sub = sortedByPowerlevel[i];

        if (minPowerlevelInGroup == null)
        {
          minPowerlevelInGroup = sub.Powerlevel;
        }

        bool powerlevelDifferenceTooBig = (sub.Powerlevel - minPowerlevelInGroup) > PowerlevelDifferenceSplit;
        bool canBeDivided = (group.Count >= MinSize && powerlevelDifferenceTooBig) || group.Count >= MaxSize;
        bool remainingAreValidDivisionSize = (sortedByPowerlevel.Count - i) >= MinSize;

        if (canBeDivided && remainingAreValidDivisionSize)
        {
          groups.Add(group);

          group = new Group<int, LeagueSubscription>();
          minPowerlevelInGroup = null;
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

    private int CalculatePowerlevel(Fighter fighter)
    {
      var stats = fighterStatCalculator.Calculate(fighter);

      return (int)(stats.OffensivePowerLevel() + stats.DefensivePowerLevel());
    }

    private class Group<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
      public TKey Key { get; set; }
    }
  }
}
