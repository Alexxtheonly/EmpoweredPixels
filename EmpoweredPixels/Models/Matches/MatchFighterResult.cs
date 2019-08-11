using System;
using EmpoweredPixels.Enums.Matches;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Models.Matches
{
  public class MatchFighterResult
  {
    public Guid FighterId { get; set; }

    public Guid MatchId { get; set; }

    public int Position { get; set; }

    public Result Result { get; set; }

    public virtual Fighter Fighter { get; set; }

    public virtual Match Match { get; set; }
  }
}
