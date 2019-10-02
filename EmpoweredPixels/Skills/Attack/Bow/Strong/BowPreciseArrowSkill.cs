using System;

namespace EmpoweredPixels.Skills.Attack.Bow.Strong
{
  public class BowPreciseArrowSkill : BowSkillBase
  {
    public override Guid Id => new Guid("02BE7CA2-7290-4150-8EC3-727A2A6A13A0");

    public override string Name => "Precise Arrow";

    public override int DamageLow => 8;

    public override int DamageHigh => 12;

    public override int Cooldown => 1;
  }
}
