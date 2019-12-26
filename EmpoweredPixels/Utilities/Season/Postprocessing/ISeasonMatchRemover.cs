using System.Threading.Tasks;
using EmpoweredPixels.Models;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public interface ISeasonMatchRemover
  {
    Task RemoveAsync(DatabaseContext context);
  }
}
