using System.Threading.Tasks;
using EmpoweredPixels.Models;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public interface ISeasonRewardClaimer
  {
    Task ClaimAsync(DatabaseContext context, long userId);
  }
}
