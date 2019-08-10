using System;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Models.Matches
{
  public class MatchRegistration
  {
    public Guid MatchId { get; set; }

    public Guid FighterId { get; set; }

    public Guid? TeamId { get; set; }

    public DateTimeOffset Date { get; set; }

    public virtual Match Match { get; set; }

    public virtual Fighter Fighter { get; set; }

    public virtual MatchTeam Team { get; set; }
  }
}
