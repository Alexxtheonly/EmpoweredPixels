using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Buffs;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Gaive.Strong
{
  public class GlaiveWhirlwindSkill : GlaiveSkillBase
  {
    public override Guid Id => new Guid("7718F8B4-CCB8-4F95-89A8-F3727A7F59D6");

    public override string Name => "Whirlwind";

    public override int DamageLow => 10;

    public override int DamageHigh => 14;

    public override int Cooldown => 2;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyKnockback(actor, 4),
        actor.ApplyBuff(actor, 100, new ReflectSkillBuff(actor) { Remaining = 2 }),
      };
    }
  }
}
