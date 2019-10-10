using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Conditions;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Strong
{
  public class GreatswordPowerfulStrikeSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("73197285-CC39-4D73-918A-CAB70EF67849");

    public override string Name => "Powerful Strike";

    public override int DamageLow => 9;

    public override int DamageHigh => 13;

    public override int Cooldown => 2;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCondition(actor, 80, new VulnerabilitySkillCondition(actor)),
      };
    }
  }
}
