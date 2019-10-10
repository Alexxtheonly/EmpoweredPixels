using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Conditions;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Gaive.Strong
{
  public class GlaivePierceSkill : GlaiveSkillBase
  {
    public override Guid Id => new Guid("2FC48C91-B48F-4DF1-845B-CF8E669CABBD");

    public override string Name => "Pierce";

    public override int DamageLow => 8;

    public override int DamageHigh => 10;

    public override int Cooldown => 1;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCondition(actor, 100, new VulnerabilitySkillCondition(actor) { Remaining = 2 }),
        target.ApplyCondition(actor, 80, new VulnerabilitySkillCondition(actor) { Remaining = 1 }),
        target.ApplyCondition(actor, 90, new BleedSkillCondition(actor) { Remaining = 1 }),
      };
    }
  }
}
