﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Utilities.FighterProgress;
using Microsoft.EntityFrameworkCore;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchFighterExperienceProcessor : IMatchFighterExperienceProcessor
  {
    private readonly DatabaseContext databaseContext;
    private readonly IFighterExperienceCalculator fighterExperienceCalculator;
    private readonly IFighterLevelUpHandler fighterLevelUpHandler;

    public MatchFighterExperienceProcessor(
      DatabaseContext databaseContext,
      IFighterExperienceCalculator fighterExperienceCalculator,
      IFighterLevelUpHandler fighterLevelUpHandler)
    {
      this.databaseContext = databaseContext;
      this.fighterExperienceCalculator = fighterExperienceCalculator;
      this.fighterLevelUpHandler = fighterLevelUpHandler;
    }

    public async Task Process(Match match, IEnumerable<FighterContribution> contributions)
    {
      var maxLevelInMatch = match.Registrations.Max(o => o.Fighter.Level);

      foreach (var contribution in contributions)
      {
        var fighter = await databaseContext.Fighters
          .AsTracking()
          .FirstOrDefaultAsync(o => o.Id == contribution.FighterId);
        if (fighter == null)
        {
          continue;
        }

        var fighterExperience = await databaseContext.FighterExperiences
          .AsTracking()
          .FirstOrDefaultAsync(o => o.FighterId == contribution.FighterId);

        if (fighterExperience == null)
        {
          fighterExperience = new FighterExperience()
          {
            FighterId = contribution.FighterId,
          };
          databaseContext.FighterExperiences.Add(fighterExperience);
        }

        var levelBefore = fighterExperienceCalculator.GetLevel(fighterExperience);

        // to boost low level fighters facing high level fighters we use this multiplicator to give them more experience
        // based on level difference
        var levelDifference = maxLevelInMatch - fighter.Level;

        // only boost if difference is > 2 levels
        var multiplicator = Math.Max(levelDifference - 2, 0);
        fighterExperienceCalculator.AddExperience(fighterExperience, contribution, multiplicator);
        var levelAfter = fighterExperienceCalculator.GetLevel(fighterExperience);

        if (levelBefore.Level < levelAfter.Level)
        {
          databaseContext.AddRange(fighterLevelUpHandler.Up(fighter));
        }
      }

      await databaseContext.SaveChangesAsync();
    }
  }
}
