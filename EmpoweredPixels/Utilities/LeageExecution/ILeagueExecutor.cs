using System.Threading.Tasks;

namespace EmpoweredPixels.Utilities.LeageExecution
{
  public interface ILeagueExecutor
  {
    Task Execute(int leagueId);
  }
}
