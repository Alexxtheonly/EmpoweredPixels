using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Enums.Matches;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Skills;
using SharpFightingEngine.Skills.Melee;
using SharpFightingEngine.Skills.Range;

namespace EmpoweredPixels.Extensions
{
  public static class DatabaseContextExtension
  {
    public static async Task StartMatch(this DatabaseContext context, Match match, IDateTimeProvider dateTimeProvider, IEngineFactory engineFactory)
    {
      match.Started = dateTimeProvider.Now;

      var fighters = match.Registrations
        .Select(o => new AdvancedFighter()
        {
          Id = o.Fighter.Id,
          Team = o.TeamId,
          Stats = new Stats()
          {
            Accuracy = o.Fighter.Accuracy,
            Agility = o.Fighter.Agility,
            Expertise = o.Fighter.Expertise,
            Power = o.Fighter.Power,
            Regeneration = o.Fighter.Regeneration,
            Speed = o.Fighter.Speed,
            Stamina = o.Fighter.Stamina,
            Toughness = o.Fighter.Toughness,
            Vision = o.Fighter.Vision,
            Vitality = o.Fighter.Vitality,
          }
        })
        .ToList();

      var skills = new ISkill[]
      {
        new StrongSmash(),
        new BleedingSmash(),
        new PoisonSmash(),
        new BurningSmash(),
        new FreezingSmash(),
        new CripplingSmash(),
        new PullingSmash(),
        new StunningSmash(),
        new StrongArrow(),
        new BleedingArrow(),
        new BurningArrow(),
        new CripplingArrow(),
        new FreezingArrow(),
        new KnockbackArrow(),
        new PoisionArrow(),
        new StrongArrow(),
        new StunningArrow(),
      };

      foreach (var fighter in fighters)
      {
        fighter.Skills = skills;
      }

      var engine = engineFactory.GetEngine(fighters, match.Options);
      var result = engine.StartMatch();

      await context.CreateFighterResults(match, result);
      await context.CreateFighterScores(match, result, dateTimeProvider);
      context.MatchResults.Add(new Models.Matches.MatchResult()
      {
        MatchId = match.Id,
        ResultJson = JsonConvert.SerializeObject(result.AsDto(), new JsonSerializerSettings()
        {
          ContractResolver = new CamelCasePropertyNamesContractResolver(),
        }).Compress(),
      });
    }

    public static async Task CreateFighterScores(this DatabaseContext context, Match match, IMatchResult result, IDateTimeProvider dateTimeProvider)
    {
      foreach (var fighterScore in result.Scores)
      {
        if (!await context.Fighters.AnyAsync(o => o.Id == fighterScore.Id))
        {
          continue;
        }

        context.MatchScoreFighters.Add(new MatchScoreFighter()
        {
          Created = dateTimeProvider.Now,
          FighterId = fighterScore.Id,
          MatchId = match.Id,
          RoundsAlive = fighterScore.RoundsAlive,
          Powerlevel = fighterScore.Powerlevel,
          TotalDamageDone = fighterScore.TotalDamageDone,
          MaxEnergy = fighterScore.MaxEnergy,
          MaxHealth = fighterScore.MaxHealth,
          TotalDamageTaken = fighterScore.TotalDamageTaken,
          TotalDeaths = fighterScore.TotalDeaths,
          TotalDistanceTraveled = fighterScore.TotalDistanceTraveled,
          TotalEnergyUsed = fighterScore.TotalEnergyUsed,
          TotalKills = fighterScore.TotalKills,
          TotalRegeneratedEnergy = fighterScore.TotalRegeneratedEnergy,
          TotalRegeneratedHealth = fighterScore.TotalRegeneratedHealth,
        });
      }
    }

    public static async Task CreateFighterResults(this DatabaseContext context, Match match, IMatchResult result)
    {
      await CreateFighterResult(result.Wins, Result.Win, match, context);
      await CreateFighterResult(result.Draws, Result.Draw, match, context);
      await CreateFighterResult(result.Loses, Result.Lose, match, context);
    }

    private static async Task CreateFighterResult(IEnumerable<IFighter> fighters, Result result, Match match, DatabaseContext context)
    {
      for (int i = 0; i < fighters.Count(); i++)
      {
        var fighter = fighters.ElementAt(i);

        if (!await context.Fighters.AnyAsync(o => o.Id == fighter.Id))
        {
          continue;
        }

        context.Add(new MatchFighterResult()
        {
          MatchId = match.Id,
          FighterId = fighter.Id,
          Position = i,
          Result = result,
        });
      }
    }
  }
}
