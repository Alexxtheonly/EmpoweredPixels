using System;
using System.Collections.Generic;
using EmpoweredPixels.Skills.Buffs;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Buffs;

namespace EmpoweredPixels.Attunements
{
  public class EarthAttunement : AttunementBase
  {
    public override Guid Id => AttunementConstants.EarthAttunement;

    public override Guid WeakAgainstAttunementId => AttunementConstants.LightningAttunement;

    public override Guid StrongAgainstAttunementId => AttunementConstants.WaterAttunement;

    public override IEnumerable<EngineTick> Effect(IFighterStats fighter, EngineCalculationValues calculationValues)
    {
      var protectionBuff = new ProtectionSkillBuff(fighter);
      fighter.States.Add(protectionBuff);

      yield return new FighterBuffAppliedTick()
      {
        Buff = protectionBuff.AsStruct(),
        Fighter = fighter.AsStruct(),
        Target = fighter.AsStruct()
      };
    }
  }
}
