using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Seasons;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public interface ISeasonEloRewardProvider
  {
    Task ProvideAsync(DatabaseContext context, SeasonSummary summary);
  }
}
