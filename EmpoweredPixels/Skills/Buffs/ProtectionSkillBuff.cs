using System;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Skills.Buffs
{
  public class ProtectionSkillBuff : SkillBuffBase
  {
    public ProtectionSkillBuff(IFighterStats source)
      : base(source)
    {
    }

    public override Guid Id => new Guid("0BEA7329-249E-4630-A003-9154A29B300C");

    public override string Name => "Protection";

    public override int Initial => 1;

    public override float? ReflectChance => null;

    public override void Apply(IStats stats)
    {
      stats.Armor = (int)(stats.Armor * 1.25F);
    }
  }
}
