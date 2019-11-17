using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Skills.Buffs
{
  public class RegenerationSkillBuff : SkillBuffBase
  {
    public RegenerationSkillBuff(IFighterStats source)
      : base(source)
    {
    }

    public override Guid Id => new Guid("7F286156-D430-455B-9075-DEACA41A2705");

    public override string Name => "Regeneration";

    public override int Initial => 1;

    public override float? ReflectChance => null;

    public float Heal => 0.5F;

    public override IEnumerable<EngineTick> Apply(IFighterStats target, IFighterStats source, EngineCalculationValues calculationValues)
    {
      var potentialHeal = Heal * (1 + (source.GetAdjustedStats().HealingPower * calculationValues.HealingPowerFactor));
      var actualHeal = target.Heal((int)potentialHeal);

      if (actualHeal == 0)
      {
        yield break;
      }

      target.DamageTaken -= actualHeal;

      yield return new FighterRegenerateHealthTick()
      {
        Fighter = target.AsStruct(),
        HealthPointsRegenerated = actualHeal,
      };
    }
  }
}
