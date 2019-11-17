using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterRegeneratedHealthTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public int HealthPointsRegenerated { get; set; }
  }
}
