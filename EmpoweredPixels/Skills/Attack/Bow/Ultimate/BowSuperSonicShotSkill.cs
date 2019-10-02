using System;

namespace EmpoweredPixels.Skills.Attack.Bow.Ultimate
{
  public class BowSuperSonicShotSkill : BowSkillBase
  {
    public override Guid Id => new Guid("27ECE055-0F60-4FA7-A5CB-849AC9588BB3");

    public override string Name => "Super Sonic Shot";

    public override int DamageLow => 35;

    public override int DamageHigh => 50;

    public override int Cooldown => 5;
  }
}
