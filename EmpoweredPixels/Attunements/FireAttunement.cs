using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Attunements
{
  public class FireAttunement : AttunementBase
  {
    public override Guid Id => AttunementConstants.FireAttunement;

    public override Guid WeakAgainstAttunementId => AttunementConstants.WaterAttunement;

    public override Guid StrongAgainstAttunementId => AttunementConstants.WindAttunement;

    public override IEnumerable<EngineTick> Attack(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      if (90F.Chance())
      {
        yield break;
      }

      var burnCondition = new BurnSkillCondition(actor) { Remaining = 1 };
      target.States.Add(burnCondition);

      yield return new FighterConditionAppliedTick()
      {
        Fighter = actor.AsStruct(),
        Condition = burnCondition.AsStruct(),
        Target = target.AsStruct(),
      };
    }
  }
}
