using System;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Basic
{
  public class GreatswordBlowSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("6450E6A7-A1EC-4B05-BB0D-C8FDBE482EAE");

    public override string Name => "Blow";

    public override int DamageLow => 5;

    public override int DamageHigh => 8;

    public override int Cooldown => 0;
  }
}
