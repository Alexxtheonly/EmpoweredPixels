using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Ultimate
{
  public class GreatswordEvisceratingCutSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("954D02EC-92C2-4B0D-9A2F-EA3BF4704FD7");

    public override string Name => "Eviscerating Cut";

    public override int DamageLow => 35;

    public override int DamageHigh => 40;

    public override int Cooldown => 5;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new List<EngineTick>
      {
        target.ApplyCondition(actor, 20, new StunSkillCondition(actor) { Remaining = 0 }),
        target.ApplyBleeding(actor, 100)
      };
    }
  }
}
