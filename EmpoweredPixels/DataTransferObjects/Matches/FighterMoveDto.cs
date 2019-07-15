using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterMoveDto : TickDto
  {
    public Guid FighterId { get; set; }

    public float CurrentX { get; set; }

    public float CurrentY { get; set; }

    public float CurrentZ { get; set; }

    public float NextX { get; set; }

    public float NextY { get; set; }

    public float NextZ { get; set; }
  }
}
