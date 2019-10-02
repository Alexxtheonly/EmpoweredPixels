using EmpoweredPixels.Models.Roster;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Utilities.FighterStatCalculation
{
  public interface IFighterStatCalculator
  {
    IStats Calculate(Fighter fighter);
  }
}
