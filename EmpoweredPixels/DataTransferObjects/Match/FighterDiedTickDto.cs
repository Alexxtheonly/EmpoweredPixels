using System;

namespace EmpoweredPixels.DataTransferObjects.Match
{
  public class FighterDiedTickDto : TickDto
  {
    public bool Died { get; set; } = true;

    public Guid FighterId { get; set; }
  }
}
