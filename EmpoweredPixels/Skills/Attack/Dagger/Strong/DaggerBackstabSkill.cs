using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Dagger.Strong
{
  public class DaggerBackstabSkill : DaggerSkillBase
  {
    public override Guid Id => new Guid("D269BB3A-D3AE-4A42-A1DC-28F557B78F4D");

    public override string Name => "Backstab";

    public override int DamageLow => 9;

    public override int DamageHigh => 12;

    public override int Cooldown => 1;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCondition(actor, 70, new BleedSkillCondition(actor) { Remaining = 2 }),
        target.ApplyCondition(actor, 5, new PoisonSkillCondition(actor) { Remaining = 0 }),
      };
    }
  }
}
