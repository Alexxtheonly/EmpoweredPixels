using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Ratings;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.EloCalculation;
using EmpoweredPixels.Utilities.FighterProgress;
using EmpoweredPixels.Utilities.FighterSkillSelection;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using EmpoweredPixels.Utilities.RewardTrackCalculation;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Extensions
{
  // TODO: refactor start match in smaller parts like prepare match, start match and post process match, maybe even more
  public static class DatabaseContextExtension
  {
    public static async Task StartMatch(
      this DatabaseContext context,
      Match match,
      IDateTimeProvider dateTimeProvider,
      IEngineFactory engineFactory,
      IContributionPointCalculator contributionPointCalculator,
      IEloCalculator eloCalculator,
      IFighterExperienceCalculator fighterExperienceCalculator,
      IFighterLevelUpHandler fighterLevelUpHandler,
      IFighterStatCalculator fighterStatCalculator,
      IFighterSkillSelector fighterSkillSelector,
      IRewardTrackCalculator rewardTrackCalculator,
      bool isExperienceeligable)
    {
      match.Started = dateTimeProvider.Now;

      var fighters = match.Registrations
        .Select(o => new AdvancedFighter()
        {
          Id = o.Fighter.Id,
          Team = o.TeamId,
          Stats = fighterStatCalculator.Calculate(o.Fighter),
          Skills = fighterSkillSelector.GetSkills(o.Fighter),
        })
        .ToList();

      var engine = engineFactory.GetEngine(fighters, match.Options);
      var result = await Task.Run(() => engine.StartMatch());

      await context.CreateFighterScores(match, result, dateTimeProvider);
      await context.UpdateFighterElos(result, contributionPointCalculator, eloCalculator, dateTimeProvider);
      if (isExperienceeligable)
      {
        await context.UpdateFighterExperiences(result, fighterExperienceCalculator, fighterLevelUpHandler);
        await context.UpdateRewardTracks(result, contributionPointCalculator, rewardTrackCalculator);
      }

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
          TotalKills = fighterScore.TotalKills,
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

    public static async Task UpdateFighterExperiences(
      this DatabaseContext context,
      IMatchResult result,
      IFighterExperienceCalculator fighterExperienceCalculator,
      IFighterLevelUpHandler fighterLevelUpHandler)
    {
      foreach (var contribution in result.Contributions)
      {
        var fighter = await context.Fighters
          .AsTracking()
          .FirstOrDefaultAsync(o => o.Id == contribution.FighterId);
        if (fighter == null)
        {
          continue;
        }

        var fighterExperience = await context.FighterExperiences
          .AsTracking()
          .FirstOrDefaultAsync(o => o.FighterId == contribution.FighterId);

        if (fighterExperience == null)
        {
          fighterExperience = new FighterExperience()
          {
            FighterId = contribution.FighterId,
          };
          context.FighterExperiences.Add(fighterExperience);
        }

        var levelBefore = fighterExperienceCalculator.GetLevel(fighterExperience);
        fighterExperienceCalculator.AddExperience(fighterExperience, contribution);
        var levelAfter = fighterExperienceCalculator.GetLevel(fighterExperience);

        if (levelBefore.Level < levelAfter.Level)
        {
          context.AddRange(fighterLevelUpHandler.Up(fighter));
        }
      }
    }

    public static async Task UpdateRewardTracks(
      this DatabaseContext context,
      IMatchResult result,
      IContributionPointCalculator contributionPointCalculator,
      IRewardTrackCalculator rewardTrackCalculator)
    {
      foreach (var contribution in result.Contributions)
      {
        var fighter = await context.Fighters.FirstOrDefaultAsync(o => o.Id == contribution.FighterId);
        if (fighter == null)
        {
          continue;
        }

        var points = contributionPointCalculator.Calculate(contribution);
        await rewardTrackCalculator.Calculate(fighter, points);
      }
    }
  }
}
