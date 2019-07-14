using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterRegeneratedEnergyDto : TickDto
  {
    public Guid FighterId { get; set; }

    public int RegeneratedEnergy { get; set; }
  }
}
