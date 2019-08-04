using System;
using System.Linq;
using EmpoweredPixels.DataTransferObjects.Matches;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.Ticks;

namespace EmpoweredPixels.Extensions
{
  public static class IMatchResultExtension
  {
    public static MatchResultDto AsDto(this IMatchResult result)
    {
      return new MatchResultDto()
      {
        Ticks = result.Ticks.Select(o => o.AsDto()),
        Scores = result.Scores.Select(o => o.AsDto()),
        TeamScores = result.TeamScores.Select(o => o.AsDto()),
      };
    }

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
      else
      {
        throw new ArgumentException($"No suitable dto for type {engineTick.GetType().Name} found.");
      }
    }

    public static RoundTickDto AsDto(this EngineRoundTick tick)
    {
      return new RoundTickDto()
      {
        Round = tick.Round,
        Ticks = tick.Ticks.Select(o => o.AsDto()),
        Scores = tick.ScoreTick.Select(o => o.AsDto()),
      };
    }

    public static RoundScoreDto AsDto(this IEngineRoundScoreTick engineTick)
    {
      if (engineTick.GetType() == typeof(EngineRoundScoreTick))
      {
        var tick = (EngineRoundScoreTick)engineTick;
        return new RoundScoreDto()
        {
          Id = tick.FighterId,
          Powerlevel = tick.Powerlevel,
          DamageDone = tick.DamageDone,
          DamageTaken = tick.DamageTaken,
          Deaths = tick.Deaths,
          DistanceTraveled = tick.DistanceTraveled,
          Energy = tick.Energy,
          EnergyUsed = tick.EnergyUsed,
          Health = tick.Health,
          Kills = tick.Kills,
          RestoredEnergy = tick.RestoredEnergy,
          RestoredHealth = tick.RestoredHealth,
          Round = tick.Round,
        };
      }
      else if (engineTick.GetType() == typeof(EngineRoundTeamScoreTick))
      {
        var tick = (EngineRoundTeamScoreTick)engineTick;
        return new RoundScoreDto()
        {
          Id = tick.TeamId,
          Powerlevel = tick.Powerlevel,
          DamageDone = tick.DamageDone,
          DamageTaken = tick.DamageTaken,
          Deaths = tick.Deaths,
          DistanceTraveled = tick.DistanceTraveled,
          Energy = tick.Energy,
          EnergyUsed = tick.EnergyUsed,
          Health = tick.Health,
          Kills = tick.Kills,
          RestoredEnergy = tick.RestoredEnergy,
          RestoredHealth = tick.RestoredHealth,
          Round = tick.Round,
        };
      }
      else
      {
        throw new ArgumentException($"No suitable dto for type {engineTick.GetType().Name} found.");
      }
    }

    public static MatchScoreDto AsDto(this MatchScore score)
    {
      return new MatchScoreDto()
      {
        Id = score.Id,
        MaxEnergy = score.MaxEnergy,
        MaxHealth = score.MaxHealth,
        Powerlevel = score.Powerlevel,
        TotalDamageDone = score.TotalDamageDone,
        TotalDamageTaken = score.TotalDamageTaken,
        TotalDeaths = score.TotalDeaths,
        TotalDistanceTraveled = score.TotalDistanceTraveled,
        TotalEnergyUsed = score.TotalEnergyUsed,
        TotalKills = score.TotalKills,
        TotalRegeneratedEnergy = score.TotalRegeneratedEnergy,
        TotalRegeneratedHealth = score.TotalRegeneratedHealth,
        RoundsAlive = score.RoundsAlive,
      };
    }
  }
}
