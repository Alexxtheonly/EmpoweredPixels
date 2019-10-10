using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Dagger.Basic
{
  public class DaggerSliceSkill : DaggerSkillBase
  {
    public override Guid Id => new Guid("8610F19B-2B90-472D-BDA8-519748E6DBC6");

    public override string Name => "Slice";

    public override int DamageLow => 3;

    public override int DamageHigh => 5;

    public override int Cooldown => 0;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCondition(actor, 80, new BleedSkillCondition(actor) { Remaining = 0 }),
      };
    }
  }
}
