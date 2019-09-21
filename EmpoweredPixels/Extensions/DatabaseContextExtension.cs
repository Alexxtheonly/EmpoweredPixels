using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Ratings;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.EloCalculation;
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
    public static async Task StartMatch(
      this DatabaseContext context,
      Match match,
      IDateTimeProvider dateTimeProvider,
      IEngineFactory engineFactory,
      IContributionPointCalculator contributionPointCalculator,
      IEloCalculator eloCalculator)
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
      var result = await Task.Run(() => engine.StartMatch());

      await context.CreateFighterScores(match, result, dateTimeProvider);
      await context.UpdateFighterElos(result, contributionPointCalculator, eloCalculator, dateTimeProvider);

      context.MatchResults.Add(new Models.Matches.MatchResult()
      {
        MatchId = match.Id,
        RoundTicks = JsonConvert.SerializeObject(result.Ticks.AsDto(), new JsonSerializerSettings()
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
          TotalDamageDone = fighterScore.TotalDamageDone,
          TotalDamageTaken = fighterScore.TotalDamageTaken,
          TotalDeaths = fighterScore.TotalDeaths,
          TotalDistanceTraveled = fighterScore.TotalDistanceTraveled,
          TotalEnergyUsed = fighterScore.TotalEnergyUsed,
          TotalKills = fighterScore.TotalKills,
          TotalRegeneratedEnergy = fighterScore.TotalRegeneratedEnergy,
          TotalRegeneratedHealth = fighterScore.TotalRegeneratedHealth,
        });

        var contribution = result.Contributions.FirstOrDefault(o => o.FighterId == fighterScore.Id);

        context.MatchContributions.Add(new MatchContribution()
        {
          FighterId = fighterScore.Id,
          HasWon = contribution.HasWon,
          KillsAndAssists = contribution.KillsAndAssists,
          MatchId = match.Id,
          MatchParticipation = contribution.MatchParticipation,
          PercentageOfRoundsAlive = contribution.PercentageOfRoundsAlive,
        });
      }
    }

    public static async Task UpdateFighterElos(
      this DatabaseContext context,
      IMatchResult result,
      IContributionPointCalculator contributionPointCalculator,
      IEloCalculator eloCalculator,
      IDateTimeProvider dateTimeProvider)
    {
      var positions = new List<EloPosition>();
      var ratings = new List<IEloRating>();
      foreach (var contribution in result.Contributions)
      {
        if (!await context.Fighters.AnyAsync(o => o.Id == contribution.FighterId))
        {
          continue;
        }

        positions.Add(new EloPosition()
        {
          Id = contribution.FighterId,
          Points = contributionPointCalculator.Calculate(contribution),
        });

        var eloRating = await context.FighterEloRatings
          .AsTracking()
          .FirstOrDefaultAsync(o => o.FighterId == contribution.FighterId);

        if (eloRating == null)
        {
          eloRating = new FighterEloRating()
          {
            FighterId = contribution.FighterId,
            CurrentElo = 1500,
            PreviousElo = 1500,
          };
          context.FighterEloRatings.Add(eloRating);
        }

        eloRating.LastUpdate = dateTimeProvider.Now;

        ratings.Add(eloRating);
      }

      eloCalculator.Calculate(ratings, positions);
    }
  }
}
