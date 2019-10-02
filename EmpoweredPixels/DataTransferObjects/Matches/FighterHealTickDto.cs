using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterHealTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public Guid TargetId { get; set; }

    public Guid HealSkillId { get; set; }

    public int PotentialHealing { get; set; }

    public int AppliedHealing { get; set; }

    public bool OutOfRange { get; set; }

    public bool OnCooldown { get; set; }
  }
}
