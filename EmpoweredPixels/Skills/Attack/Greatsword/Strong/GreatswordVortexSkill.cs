using System;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Strong
{
  public class GreatswordVortexSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("5DA1FDF2-3060-4C18-8613-162C8DF475D2");

    public override string Name => "Vortex";

    public override int DamageLow => 5;

    public override int DamageHigh => 8;

    public override int Cooldown => 1;
  }
}
