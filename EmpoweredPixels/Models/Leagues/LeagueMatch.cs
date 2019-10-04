using System;
using EmpoweredPixels.Models.Matches;

namespace EmpoweredPixels.Models.Leagues
{
  public class LeagueMatch
  {
    public int LeagueId { get; set; }

    public Guid MatchId { get; set; }

    public int Division { get; set; }

    public virtual League League { get; set; }

    public virtual Match Match { get; set; }
  }
}
