using System.Collections.Generic;
using EmpoweredPixels.Models.Rewards;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Utilities.FighterProgress
{
  public interface IFighterLevelUpHandler
  {
    IEnumerable<Reward> Up(Fighter fighter);
  }
}
