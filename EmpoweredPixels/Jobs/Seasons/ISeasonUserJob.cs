using System.Threading.Tasks;

namespace EmpoweredPixels.Jobs.Seasons
{
  public interface ISeasonUserJob
  {
    Task ExecuteAsync(int seasonId, long userId);
  }
}
