using System;

namespace EmpoweredPixels.Skills.Attack.Bow.Basic
{
  public class BowShotSkill : BowSkillBase
  {
    public override Guid Id => new Guid("27CCB150-9EA2-4FD1-8DFC-EB13E512C225");

    public override string Name => "Shot";

    public override int DamageLow => 4;

    public override int DamageHigh => 6;

    public override int Cooldown => 0;
  }
}
