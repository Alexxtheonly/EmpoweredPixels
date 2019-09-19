using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterMovedByAttackTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public Guid TargetId { get; set; }

    public float CurrentX { get; set; }

    public float CurrentY { get; set; }

    public float NextX { get; set; }

    public float NextY { get; set; }
  }
}
