﻿using System;
using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.DataTransferObjects.Matches;
using SharpFightingEngine.Engines.Ticks;

namespace EmpoweredPixels.Extensions
{
  public static class IMatchResultExtension
  {
    public static TickDto AsDto(this EngineTick engineTick)
    {
      if (engineTick.GetType() == typeof(FighterAttackTick))
      {
        var tick = (FighterAttackTick)engineTick;
        return new FighterAttackDto()
        {
          FighterId = tick.Fighter.Id,
          TargetId = tick.Target.Id,
          SkillId = tick.Skill.Id,
          Damage = tick.Damage,
          Energy = tick.Energy,
          Critical = tick.Critical,
          Dodged = tick.Dodged,
          InsufficientEnergy = tick.InsufficientEnergy,
          OutOfRange = tick.OutOfRange,
        };
      }
      else if (engineTick.GetType() == typeof(FighterMoveTick))
      {
        var tick = (FighterMoveTick)engineTick;
        return new FighterMoveDto()
        {
          FighterId = tick.Fighter.Id,
          CurrentX = tick.Fighter.X,
          CurrentY = tick.Fighter.Y,
          CurrentZ = tick.Fighter.Z,
          NextX = tick.Next.X,
          NextY = tick.Next.Y,
          NextZ = tick.Next.Z,
        };
      }
      else if (engineTick.GetType() == typeof(EngineFighterDiedTick))
      {
        var tick = (EngineFighterDiedTick)engineTick;
        return new FighterDiedTickDto()
        {
          FighterId = tick.Fighter.Id,
        };
      }
      else if (engineTick.GetType() == typeof(FighterRegenerateHealthTick))
      {
        var tick = (FighterRegenerateHealthTick)engineTick;
        return new FighterRegeneratedHealthDto()
        {
          FighterId = tick.Fighter.Id,
          HealthPointsRegenerated = tick.HealthPointsRegenerated,
        };
      }
      else if (engineTick.GetType() == typeof(FighterRegenerateEnergyTick))
      {
        var tick = (FighterRegenerateEnergyTick)engineTick;
        return new FighterRegeneratedEnergyDto()
        {
          FighterId = tick.Fighter.Id,
          RegeneratedEnergy = tick.RegeneratedEnergy,
        };
      }
      else if (engineTick.GetType() == typeof(FighterSpawnTick))
      {
        var tick = (FighterSpawnTick)engineTick;
        return new FighterSpawnTickDto()
        {
          FighterId = tick.Fighter.Id,
          Health = tick.Fighter.Health,
          Energy = tick.Fighter.Energy,
          PositionX = tick.Fighter.X,
          PositionY = tick.Fighter.Y,
          PositionZ = tick.Fighter.Z,
        };
      }
      else if (engineTick.GetType() == typeof(FighterSacrificedTick))
      {
        var tick = (FighterSacrificedTick)engineTick;
        return new FighterSacrificedTickDto()
        {
          FighterId = tick.Fighter.Id,
        };
      }
      else if (engineTick.GetType() == typeof(FighterMovedByAttackTick))
      {
        var tick = (FighterMovedByAttackTick)engineTick;
        return new FighterMovedByAttackTickDto()
        {
          FighterId = tick.Fighter.Id,
          TargetId = tick.Target.Id,
          CurrentX = tick.Current.X,
          CurrentY = tick.Current.Y,
          NextX = tick.Next.X,
          NextY = tick.Next.Y,
        };
      }
      else if (engineTick.GetType() == typeof(FighterConditionAppliedTick))
      {
        var tick = (FighterConditionAppliedTick)engineTick;
        return new FighterConditionAppliedTickDto()
        {
          ConditionId = tick.Condition.Id,
          FighterId = tick.Fighter.Id,
          TargetId = tick.Target.Id,
        };
      }
      else if (engineTick.GetType() == typeof(FighterConditionDamageTick))
      {
        var tick = (FighterConditionDamageTick)engineTick;
        return new FighterConditionDamageTickDto()
        {
          ConditionId = tick.Condition.Id,
          Damage = tick.Damage,
          FighterId = tick.Fighter.Id,
        };
      }
      else if (engineTick.GetType() == typeof(FighterStunnedTick))
      {
        var tick = (FighterStunnedTick)engineTick;
        return new FighterStunnedTickDto()
        {
          FighterId = tick.Fighter.Id,
        };
      }
      else
      {
        throw new ArgumentException($"No suitable dto for type {engineTick.GetType().Name} found.");
      }
    }

    public static IEnumerable<RoundTickDto> AsDto(this IEnumerable<EngineRoundTick> roundTicks)
    {
      return roundTicks
        .Select(o => o.AsDto());
    }

    public static RoundTickDto AsDto(this EngineRoundTick tick)
    {
      return new RoundTickDto()
      {
        Round = tick.Round,
        Ticks = tick.Ticks.Select(o => o.AsDto()),
      };
    }
  }
}
