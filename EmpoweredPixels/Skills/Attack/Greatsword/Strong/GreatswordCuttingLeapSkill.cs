using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Extensions;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Skills.Attack.Greatsword.Strong
{
  public class GreatswordCuttingLeapSkill : GreatswordSkillBase
  {
    public override Guid Id => new Guid("050438AD-DE49-40F9-8B3B-649D89954D3F");

    public override string Name => "Cutting Leap";

    public override int DamageLow => 5;

    public override int DamageHigh => 8;

    public override float Range => 12;

    public override int Cooldown => 2;

    public override IEnumerable<EngineTick> Perform(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return target.ApplyCharge(actor, 10).Yield();
    }
  }
}
