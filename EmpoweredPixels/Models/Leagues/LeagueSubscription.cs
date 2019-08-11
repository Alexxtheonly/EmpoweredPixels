using System;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Models.Leagues
{
  public class LeagueSubscription
  {
    public int LeagueId { get; set; }

    public Guid FighterId { get; set; }

    public virtual League League { get; set; }

    public virtual Fighter Fighter { get; set; }
  }
}
