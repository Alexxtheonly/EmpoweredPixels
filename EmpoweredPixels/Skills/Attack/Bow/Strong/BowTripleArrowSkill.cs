using System;

namespace EmpoweredPixels.Skills.Attack.Bow.Strong
{
  public class BowTripleArrowSkill : BowSkillBase
  {
    public override Guid Id => new Guid("1F0BA8AE-7929-45B7-BF5C-9C2740B062B1");

    public override string Name => "Triple Arrow";

    public override int DamageLow => 12;

    public override int DamageHigh => 15;

    public override int Cooldown => 2;
  }
}
