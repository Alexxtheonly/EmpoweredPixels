using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Dagger.Strong
{
  public class DaggerJaggedBladeSkill : DaggerSkillBase
  {
    public override Guid Id => new Guid("8E2CB0C8-EB08-42A3-ABA8-F26C9F2EB418");

    public override string Name => "Jagged Blade";

    public override int DamageLow => 8;

    public override int DamageHigh => 11;

    public override int Cooldown => 1;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCripple(actor, 80),
        target.ApplyBleeding(actor, 18),
        target.ApplyCondition(actor, 5, new PoisonSkillCondition(actor) { Remaining = 3 }),
      };
    }
  }
}
