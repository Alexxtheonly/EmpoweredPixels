using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterConditionDamageTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public Guid ConditionId { get; set; }

    public int Damage { get; set; }
  }
}
