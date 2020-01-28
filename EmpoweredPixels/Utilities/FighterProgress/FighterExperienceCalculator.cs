using EmpoweredPixels.Extensions;
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

    public void AddExperience(FighterExperience fighterExperience, FighterContribution contribution, double mutliplicator)
    {
      var experience = contributionPointCalculator.Calculate(contribution);
      fighterExperience.Points += experience + (int)(experience * mutliplicator);
      fighterExperience.LastUpdate = dateTimeProvider.Now;
    }

    public long GetNeededExperience(int level)
    {
      return 2000 + (1000 * (level.NearestBase(8) / 8));
    }
  }
}
