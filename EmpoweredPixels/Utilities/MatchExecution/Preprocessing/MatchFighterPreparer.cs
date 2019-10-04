﻿using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Utilities.FighterSkillSelection;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchFighterPreparer : IMatchFighterPreparer
  {
    private readonly IFighterStatCalculator fighterStatCalculator;
    private readonly IFighterSkillSelector fighterSkillSelector;

    public MatchFighterPreparer(IFighterStatCalculator fighterStatCalculator, IFighterSkillSelector fighterSkillSelector)
    {
      this.fighterStatCalculator = fighterStatCalculator;
      this.fighterSkillSelector = fighterSkillSelector;
    }

    public IEnumerable<FighterBase> GetFighters(Match match)
    {
      return match.Registrations
        .Select(o => new AdvancedFighter()
        {
          Id = o.Fighter.Id,
          Team = o.TeamId,
          Stats = fighterStatCalculator.Calculate(o.Fighter),
          Skills = fighterSkillSelector.GetSkills(o.Fighter),
        }).ToList();
    }
  }
}
