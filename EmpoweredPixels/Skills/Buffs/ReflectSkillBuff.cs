using System;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Skills.Buffs
{
  public class ReflectSkillBuff : SkillBuffBase
  {
    public ReflectSkillBuff(IFighterStats source)
      : base(source)
    {
    }

    public override Guid Id => new Guid("97A1476F-2669-45E9-B511-3F5CACA1742E");

    public override string Name => "Reflect";

    public override int Initial => 1;

    public override float? ReflectChance => 75;
  }
}
