using System.Threading.Tasks;

namespace EmpoweredPixels.Jobs.Seasons
{
  public interface ISeasonInitiatorJob
  {
    Task InitAsync();
  }
}
