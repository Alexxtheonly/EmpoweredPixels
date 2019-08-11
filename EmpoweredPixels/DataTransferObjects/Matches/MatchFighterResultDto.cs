using System;
using EmpoweredPixels.Enums.Matches;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class MatchFighterResultDto
  {
    public Guid MatchId { get; set; }

    public Guid FighterId { get; set; }

    public int Position { get; set; }

    public Result Result { get; set; }
  }
}
