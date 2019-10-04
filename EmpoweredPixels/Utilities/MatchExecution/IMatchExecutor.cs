using System.Threading.Tasks;
using EmpoweredPixels.Models.Matches;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public interface IMatchExecutor
  {
    bool ProcessFighterContribution { get; set; }

    bool ProcessFighterElo { get; set; }

    bool ProcessFighterExperience { get; set; }

    bool ProcessLog { get; set; }

    bool ProcessRewardTracks { get; set; }

    bool ProcessScores { get; set; }

    Task Execute(Match match);
  }
}
