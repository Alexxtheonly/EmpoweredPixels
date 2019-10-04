using System.Collections.Generic;
using System.Threading.Tasks;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public interface IMatchFighterRewardTrackProcessor
  {
    Task Process(IEnumerable<FighterContribution> contributions);
  }
}
