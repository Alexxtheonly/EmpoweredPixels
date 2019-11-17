using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Buffs;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Buffs;

namespace EmpoweredPixels.Attunements
{
  public class WaterAttunement : AttunementBase
  {
    public override Guid Id => AttunementConstants.WaterAttunement;

    public override Guid WeakAgainstAttunementId => AttunementConstants.EarthAttunement;

    public override Guid StrongAgainstAttunementId => AttunementConstants.FireAttunement;

    public override IEnumerable<EngineTick> Effect(IFighterStats fighter, EngineCalculationValues calculationValues)
    {
      var regenerationBuff = new RegenerationSkillBuff(fighter);

      fighter.States.Add(regenerationBuff);

      yield return new FighterBuffAppliedTick()
      {
        Fighter = fighter.AsStruct(),
        Buff = regenerationBuff.AsStruct(),
        Target = fighter.AsStruct(),
      };
    }
  }
}
