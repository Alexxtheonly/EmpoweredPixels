using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.EntityFrameworkCore;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.FighterProgress
{
  public class FighterExperienceCalculator : IFighterExperienceCalculator
  {
    private const int KillExperience = 20;
    private const int AssistExperience = 10;

    private readonly IDateTimeProvider dateTimeProvider;
    private readonly DatabaseContext context;

    public FighterExperienceCalculator(IDateTimeProvider dateTimeProvider, DatabaseContext context)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.context = context;
    }

    public FighterLevel GetLevel(FighterExperience fighterExperience)
    {
      var fighterLevel = new FighterLevel
      {
        Level = 1,
      };

      long neededExp = 0;
      bool levelFound = false;

      do
      {
        var levelExp = GetNeededExperience(fighterLevel.Level);
        neededExp += levelExp;

        if (neededExp <= fighterExperience.Points)
        {
          fighterLevel.Level += 1;
        }
        else
        {
          levelFound = true;
          fighterLevel.RequiredExperience = levelExp;
          fighterLevel.Experience = levelExp + (fighterExperience.Points - neededExp);
        }
      }
      while (!levelFound);

      return fighterLevel;
    }

    public async Task AddExperienceAsync(FighterExperience fighterExperience, FighterContribution contribution, double mutliplicator)
    {
      var ids = new Guid[] { fighterExperience.FighterId }.Union(contribution.Kills).Union(contribution.Assists).ToList();
      var fighters = await context.Fighters.Where(o => ids.Contains(o.Id)).ToListAsync();

      var fighter = fighters.First(o => o.Id == fighterExperience.FighterId);
      var experience = 100;

      foreach (var killFighterId in contribution.Kills)
      {
        experience += GetExperience(fighters, fighter, killFighterId, KillExperience);
      }

      foreach (var assistFighterId in contribution.Assists)
      {
        experience += GetExperience(fighters, fighter, assistFighterId, AssistExperience);
      }

      fighterExperience.Points += experience + (int)(experience * mutliplicator);
      fighterExperience.LastUpdate = dateTimeProvider.Now;
    }

    public long GetNeededExperience(int level)
    {
      return 2000 + (1000 * (level.NearestBase(8) / 8));
    }

    private int GetExperience(List<Fighter> fighters, Fighter fighter, Guid otherFighterId, int experienceValue)
    {
      return (int)(experienceValue * GetExperienceFactor(fighter.Level, fighters.First(o => o.Id == otherFighterId).Level));
    }

    private double GetExperienceFactor(int level, int otherLevel)
    {
      var factor = 1D;

      var difference = otherLevel - level;
      factor += 0.2D * difference;

      if (factor < 0)
      {
        return 0;
      }

      return factor;
    }
  }
}
