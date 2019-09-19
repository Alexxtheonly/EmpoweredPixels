using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterStunnedTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public bool IsStunned { get; set; } = true;
  }
}
