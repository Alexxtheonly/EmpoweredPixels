using System.Threading.Tasks;
using EmpoweredPixels.Models.Roster;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.FighterProgress
{
  public interface IFighterExperienceCalculator
  {
    Task AddExperienceAsync(FighterExperience fighterExperience, FighterContribution contribution, double mutliplicator);

    FighterLevel GetLevel(FighterExperience fighterExperience);

    long GetNeededExperience(int level);
  }
}
