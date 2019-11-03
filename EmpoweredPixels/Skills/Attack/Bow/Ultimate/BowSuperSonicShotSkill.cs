using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Bow.Ultimate
{
  public class BowSuperSonicShotSkill : BowSkillBase
  {
    public override Guid Id => new Guid("27ECE055-0F60-4FA7-A5CB-849AC9588BB3");

    public override string Name => "Super Sonic Shot";

    public override int DamageLow => 28;

    public override int DamageHigh => 40;

    public override int Cooldown => 5;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new List<EngineTick>
      {
        target.ApplyCondition(actor, 50, new StunSkillCondition(actor) { Remaining = 0 }),
        target.ApplyBleeding(actor, 100)
      };
    }
  }
}
