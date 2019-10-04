using System.Threading.Tasks;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public interface IMatchPostprocessor
  {
    bool ProcessFighterContribution { get; set; }

    bool ProcessFighterElo { get; set; }

    bool ProcessFighterExperience { get; set; }

    bool ProcessRewardTracks { get; set; }

    bool ProcessScores { get; set; }

    bool ProcessLog { get; set; }

    Task Process(Match match, IMatchResult result);
  }
}
