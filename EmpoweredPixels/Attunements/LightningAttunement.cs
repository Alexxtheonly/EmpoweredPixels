using System;
using System.Collections.Generic;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Conditions;
using SharpFightingEngine.Utilities;

namespace EmpoweredPixels.Attunements
{
  public class LightningAttunement : AttunementBase
  {
    public override Guid Id => AttunementConstants.LightningAttunement;

    public override Guid WeakAgainstAttunementId => AttunementConstants.WindAttunement;

    public override Guid StrongAgainstAttunementId => AttunementConstants.EarthAttunement;

    public override IEnumerable<EngineTick> Attack(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      if (10F.Chance())
      {
        yield break;
      }

      var stunCondition = new StunSkillCondition(actor) { Remaining = 0 };
      target.States.Add(stunCondition);

      yield return new FighterConditionAppliedTick()
      {
        Condition = stunCondition.AsStruct(),
        Fighter = actor.AsStruct(),
        Target = target.AsStruct(),
      };
    }
  }
}
