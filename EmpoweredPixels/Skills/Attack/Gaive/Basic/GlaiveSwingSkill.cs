using System;

namespace EmpoweredPixels.Skills.Attack.Gaive.Basic
{
  public class GlaiveSwingSkill : GlaiveSkillBase
  {
    public override Guid Id => new Guid("F0AC2CBA-FBCB-4B37-A564-1F333CE58C74");

    public override string Name => "Swing";

    public override int DamageLow => 5;

    public override int DamageHigh => 7;

    public override int Cooldown => 0;
  }
}
