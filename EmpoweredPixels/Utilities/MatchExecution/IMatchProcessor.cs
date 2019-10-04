using System.Collections.Generic;
using System.Threading.Tasks;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public interface IMatchProcessor
  {
    Task<IMatchResult> Process(IEnumerable<FighterBase> fighters, Match match);
  }
}
