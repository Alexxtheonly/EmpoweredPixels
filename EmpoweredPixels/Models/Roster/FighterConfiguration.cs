using System;

namespace EmpoweredPixels.Models.Roster
{
  public class FighterConfiguration
  {
    public Guid FighterId { get; set; }

    public Guid? AttunementId { get; set; }

    public float HealThreshold { get; set; }

    public virtual Fighter Fighter { get; set; }
  }
}
