using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Models.Leagues;

namespace EmpoweredPixels.Utilities.LeageExecution
{
  public interface ILeagueDivisionDivider
  {
    IEnumerable<IGrouping<int, LeagueSubscription>> GetDivisions(IEnumerable<LeagueSubscription> subscriptions);
  }
}
