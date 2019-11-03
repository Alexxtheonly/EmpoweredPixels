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
  public class GlaiveFlyingDragonSkill : GlaiveSkillBase
  {
    public override Guid Id => new Guid("8C15FC1F-052E-4B4D-9195-D62C9E2CC5F7");

    public override string Name => "Flying Dragon";

    public override int DamageLow => 10;

    public override int DamageHigh => 12;

    public override int Cooldown => 1;

    public override bool CanBeReflected => true;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCondition(actor, 75, new StunSkillCondition(actor) { Remaining = 0 }),
        target.ApplyCondition(actor, 90, new VulnerabilitySkillCondition(actor) { Remaining = 1 }),
      };
    }
  }
}
