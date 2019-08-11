using System.Threading.Tasks;

namespace EmpoweredPixels.Jobs
{
  public interface ILeagueJob
  {
    Task RunMatchAsync(int leagueId);
  }
}
