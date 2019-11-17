using System;
using System.Collections.Generic;
using System.Linq;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Attunements
{
  public abstract class AttunementBase : IFighterAttunement
  {
    public abstract Guid Id { get; }

    public abstract Guid WeakAgainstAttunementId { get; }

    public abstract Guid StrongAgainstAttunementId { get; }

    public virtual IEnumerable<EngineTick> Attack(IFighterStats actor, IFighterStats target, EngineCalculationValues calculationValues)
    {
      return Enumerable.Empty<EngineTick>();
    }

    public virtual IEnumerable<EngineTick> Effect(IFighterStats fighter, EngineCalculationValues calculationValues)
    {
      return Enumerable.Empty<EngineTick>();
    }

    public virtual int CalculateDamageDone(IFighterAttunement target, int currentDamage)
    {
      if (target == null)
      {
        return currentDamage;
      }

      var targetAttunement = (target as AttunementBase).Id;
      var factor = 1F;

      if (StrongAgainstAttunementId == targetAttunement)
      {
        factor = 1.25F;
      }
      else if (WeakAgainstAttunementId == targetAttunement)
      {
        factor = 0.75F;
      }

      return (int)(currentDamage * factor);
    }
  }
}
