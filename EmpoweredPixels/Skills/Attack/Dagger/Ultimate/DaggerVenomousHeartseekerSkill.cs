using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Skills.Extensions;

namespace EmpoweredPixels.Skills.Attack.Dagger.Ultimate
{
  public class DaggerVenomousHeartseekerSkill : DaggerSkillBase
  {
    public override Guid Id => new Guid("44784EE9-F469-4350-8F24-D592D7318B4F");

    public override string Name => "Venomous Heartseeker";

    public override int DamageLow => 40;

    public override int DamageHigh => 45;

    public override int Cooldown => 50;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return new EngineTick[]
      {
        target.ApplyCondition(actor, 100, new PoisonSkillCondition(actor) { Remaining = 2 }),
        target.ApplyCondition(actor, 80, new PoisonSkillCondition(actor) { Remaining = 1 }),
        target.ApplyCondition(actor, 90, new BleedSkillCondition(actor) { Remaining = 1 }),
        target.ApplyCondition(actor, 100, new StunSkillCondition(actor) { Remaining = 1 }),
      };
    }
  }
}
