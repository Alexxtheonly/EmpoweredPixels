using System;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Strong
{
  public class GreatswordPowerfulStrikeSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("73197285-CC39-4D73-918A-CAB70EF67849");

    public override string Name => "Powerful Strike";

    public override int DamageLow => 7;

    public override int DamageHigh => 12;

    public override int Cooldown => 1;
  }
}
