using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Conditions;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Gaive.Ultimate
{
  public class GlaiveMultiStrikeSkill : GlaiveSkillBase
  {
    public override Guid Id => new Guid("8EF3017A-40C6-47C0-AF0E-88A4FBBBA72E");

    public override string Name => "Multi Strike";

    public override int DamageLow => 28;

    public override int DamageHigh => 40;

    public override int Cooldown => 5;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCondition(actor, 100, new VulnerabilitySkillCondition(actor) { Remaining = 2 }),
        target.ApplyCondition(actor, 80, new VulnerabilitySkillCondition(actor) { Remaining = 1 }),
      };
    }
  }
}
