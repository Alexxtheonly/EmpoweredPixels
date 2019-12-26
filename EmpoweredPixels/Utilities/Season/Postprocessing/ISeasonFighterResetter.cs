using System.Threading.Tasks;
using EmpoweredPixels.Models;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public interface ISeasonFighterResetter
  {
    Task ResetAsync(DatabaseContext context, long userId);
  }
}
