using System;

namespace EmpoweredPixels.DataTransferObjects.Roster
{
  public class FighterConfigurationDto
  {
    public Guid FighterId { get; set; }

    public Guid? AttunementId { get; set; }

    public float HealThreshold { get; set; } = 0.4F;
  }
}
