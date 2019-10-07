using EmpoweredPixels.Models.Roster;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.FighterProgress
{
  public interface IFighterExperienceCalculator
  {
    void AddExperience(FighterExperience fighterExperience, FighterContribution contribution, double mutliplicator);

    FighterLevel GetLevel(FighterExperience fighterExperience);

    long GetNeededExperience(int level);
  }
}
