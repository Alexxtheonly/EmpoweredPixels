using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Seasons;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public interface ISeasonSalvageRewardProvider
  {
    Task ProvideAsync(DatabaseContext context, SeasonSummary summary);
  }
}
