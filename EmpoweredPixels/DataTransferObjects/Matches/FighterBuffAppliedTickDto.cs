using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterBuffAppliedTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public Guid TargetId { get; set; }

    public Guid BuffId { get; set; }
  }
}
