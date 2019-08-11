using System;
using EmpoweredPixels.Enums.Matches;

namespace EmpoweredPixels.Models.Matches
{
  public class MatchFighterResult
  {
    public Guid FighterId { get; set; }

    public Guid MatchId { get; set; }

    public int Position { get; set; }

    public Result Result { get; set; }
  }
}
