using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Buffs;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Strong
{
  public class GreatswordVortexSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("5DA1FDF2-3060-4C18-8613-162C8DF475D2");

    public override string Name => "Vortex";

    public override int DamageLow => 10;

    public override int DamageHigh => 15;

    public override int Cooldown => 1;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        actor.ApplyBuff(actor, 100, new ReflectSkillBuff()),
      };
    }
  }
}
