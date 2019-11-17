using System;
using System.Collections.Generic;
using System.Linq;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills.Buffs;

namespace EmpoweredPixels.Skills.Buffs
{
  public abstract class SkillBuffBase : ISkillBuff
  {
    public SkillBuffBase(IFighterStats source)
    {
      Source = source;
    }

    public abstract Guid Id { get; }

    public abstract string Name { get; }

    public int Remaining { get; set; }

    public abstract int Initial { get; }

    public IFighterStats Source { get; }

    public abstract float? ReflectChance { get; }

    public virtual void Apply(IStats stats)
    {
    }

    public virtual IEnumerable<EngineTick> Apply(IFighterStats target, IFighterStats source, EngineCalculationValues calculationValues)
    {
      return Enumerable.Empty<EngineTick>();
    }
  }
}
