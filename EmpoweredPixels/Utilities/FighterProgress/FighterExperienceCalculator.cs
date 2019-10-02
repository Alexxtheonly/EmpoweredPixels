using System;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.FighterProgress
{
  public class FighterExperienceCalculator : IFighterExperienceCalculator
  {
    private readonly IContributionPointCalculator contributionPointCalculator;
    private readonly IDateTimeProvider dateTimeProvider;

    public FighterExperienceCalculator(IContributionPointCalculator contributionPointCalculator, IDateTimeProvider dateTimeProvider)
    {
      this.contributionPointCalculator = contributionPointCalculator;
      this.dateTimeProvider = dateTimeProvider;
    }

    public FighterLevel GetLevel(FighterExperience fighterExperience)
    {
      var fighterLevel = new FighterLevel
      {
        Level = 0,
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

    public void AddExperience(FighterExperience fighterExperience, FighterContribution contribution)
    {
      var experience = contributionPointCalculator.Calculate(contribution);
      fighterExperience.Points += experience;
      fighterExperience.LastUpdate = dateTimeProvider.Now;
    }

    public long GetNeededExperience(int level)
    {
      var factor = 2.5 * (level / 8D);

      const int experienceNeeded = 256;

      return (long)(experienceNeeded * Math.Pow(factor, 2));
    }
  }
}
