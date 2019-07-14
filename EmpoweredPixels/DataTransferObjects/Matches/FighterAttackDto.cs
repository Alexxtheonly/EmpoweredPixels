using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterAttackDto : TickDto
  {
    public Guid TargetId { get; set; }

    public Guid FighterId { get; set; }

    public Guid SkillId { get; set; }

    public int Damage { get; set; }

    public bool Dodged { get; set; }

    public bool Critical { get; set; }

    public bool OutOfRange { get; set; }

    public bool InsufficientEnergy { get; set; }
  }
}
