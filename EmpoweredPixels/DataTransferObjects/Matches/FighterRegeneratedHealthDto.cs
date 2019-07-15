using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterRegeneratedHealthDto : TickDto
  {
    public Guid FighterId { get; set; }

    public int HealthPointsRegenerated { get; set; }
  }
}
