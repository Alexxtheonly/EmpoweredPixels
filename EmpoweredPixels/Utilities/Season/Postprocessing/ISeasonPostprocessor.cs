using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Seasons;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public interface ISeasonPostprocessor
  {
    Task ProcessAsync(DatabaseContext context, SeasonProgress seasonProgress);
  }
}
