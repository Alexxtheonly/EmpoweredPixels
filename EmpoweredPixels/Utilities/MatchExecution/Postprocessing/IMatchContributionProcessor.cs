using System.Collections.Generic;
using System.Threading.Tasks;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public interface IMatchContributionProcessor
  {
    Task Process(Match match, IEnumerable<FighterContribution> contributions);
  }
}
