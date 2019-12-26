using System.Threading.Tasks;

namespace EmpoweredPixels.Utilities.Season.Preprocessing
{
  public interface ISeasonCreator
  {
    Task<Models.Seasons.Season> CreateAsync();
  }
}
