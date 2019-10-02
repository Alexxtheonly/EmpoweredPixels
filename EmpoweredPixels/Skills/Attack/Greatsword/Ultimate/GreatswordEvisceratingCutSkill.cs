using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Ultimate
{
  public class GreatswordEvisceratingCutSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("954D02EC-92C2-4B0D-9A2F-EA3BF4704FD7");

    public override string Name => "Eviscerating Cut";

    public override int DamageLow => 35;

    public override int DamageHigh => 50;

    public override int Cooldown => 5;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return target.ApplyBleeding(actor, 100).Yield();
    }
  }
}
