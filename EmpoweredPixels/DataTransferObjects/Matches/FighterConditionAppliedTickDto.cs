using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterConditionAppliedTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public Guid TargetId { get; set; }

    public Guid ConditionId { get; set; }
  }
}
