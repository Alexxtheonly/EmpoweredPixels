using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Buffs;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Buffs;

namespace EmpoweredPixels.Attunements
{
  public class WindAttunement : AttunementBase
  {
    public override Guid Id => AttunementConstants.WindAttunement;

    public override Guid WeakAgainstAttunementId => AttunementConstants.FireAttunement;

    public override Guid StrongAgainstAttunementId => AttunementConstants.LightningAttunement;

    public override IEnumerable<EngineTick> Effect(IFighterStats fighter, EngineCalculationValues calculationValues)
    {
      var powerBuff = new PowerSkillBuff(fighter);
      fighter.States.Add(powerBuff);

      yield return new FighterBuffAppliedTick()
      {
        Buff = powerBuff.AsStruct(),
        Fighter = fighter.AsStruct(),
        Target = fighter.AsStruct(),
      };
    }
  }
}
