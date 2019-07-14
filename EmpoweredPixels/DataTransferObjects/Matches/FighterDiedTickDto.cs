using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterDiedTickDto : TickDto
  {
    public bool Died { get; set; } = true;

    public Guid FighterId { get; set; }
  }
}
