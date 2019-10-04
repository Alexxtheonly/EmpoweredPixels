using System.Collections.Generic;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public interface IMatchFighterPreparer
  {
    IEnumerable<FighterBase> GetFighters(Match match);
  }
}
