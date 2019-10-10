using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Skills.Attack.Bow.Strong
{
  public class BowExplodingArrowSkill : BowSkillBase
  {
    public override Guid Id => new Guid("191D2FA1-BD89-4765-BF9B-1A02AF48287C");

    public override string Name => "Exploding Arrow";

    public override int DamageLow => 10;

    public override int DamageHigh => 13;

    public override int Cooldown => 2;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return target.ApplyKnockback(actor, 10.15F).Yield();
    }
  }
}
