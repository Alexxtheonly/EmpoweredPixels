using System;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Skills.Buffs
{
  public class PowerSkillBuff : SkillBuffBase
  {
    public PowerSkillBuff(IFighterStats source)
      : base(source)
    {
    }

    public override Guid Id => new Guid("2FBB1827-FA41-4A88-8938-359B53187361");

    public override string Name => "Power";

    public override int Initial => 1;

    public override float? ReflectChance => null;

    public override void Apply(IStats stats)
    {
      stats.Power = (int)(stats.Power * 1.1F);
    }
  }
}
