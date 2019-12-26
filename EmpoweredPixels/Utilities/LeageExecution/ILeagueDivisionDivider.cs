using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Utilities.LeageExecution
{
  public interface ILeagueDivisionDivider
  {
    IEnumerable<IGrouping<int, Fighter>> GetDivisions(IEnumerable<Fighter> fighters);
  }
}
