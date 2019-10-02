using System.Threading.Tasks;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Utilities.RewardTrackCalculation
{
  public interface IRewardTrackCalculator
  {
    Task Calculate(Fighter fighter, int points);
  }
}
