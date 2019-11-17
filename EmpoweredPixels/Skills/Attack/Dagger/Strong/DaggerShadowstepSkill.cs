using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Buffs;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Dagger.Strong
{
  public class DaggerShadowstepSkill : DaggerSkillBase
  {
    public override Guid Id => new Guid("3E466473-3A60-4EA8-AB43-A7C9EDF97022");

    public override string Name => "Shadowstep";

    public override int DamageLow => 0;

    public override int DamageHigh => 0;

    public override int Cooldown => 2;

    public override float Range => 30;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCharge(actor, Range),
        actor.ApplyBuff(actor, 75, new ReflectSkillBuff(actor) { Remaining = 1 }),
      };
    }
  }
}
