using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Utilities.LeageExecution
{
  public class EqualPartLeagueDivisionDivider : ILeagueDivisionDivider
  {
    public IEnumerable<IGrouping<int, Fighter>> GetDivisions(IEnumerable<Fighter> fighters)
    {
      var divisions = fighters.Count() / 4;
      if (divisions == 0)
      {
        divisions = 1;
      }

      int division = 1;
      foreach (var chunk in fighters
        .OrderByDescending(o => o.EloRating.CurrentElo)
        .ToList()
        .Split(divisions))
      {
        var group = new Group<int, Fighter>();
        group.AddRange(chunk);
        group.Key = division++;

        yield return group;
      }
    }

    private class Group<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
      public TKey Key { get; set; }
    }
  }
}
