using System;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Models.Matches
{
  public class MatchScoreFighter : MatchScoreBase
  {
    public Guid FighterId { get; set; }

    public Guid? TeamId { get; set; }

    public virtual Fighter Fighter { get; set; }
  }
}
