using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterSacrificedTickDto : TickDto
  {
    public bool Sacrificed { get; set; } = true;

    public Guid FighterId { get; set; }
  }
}
