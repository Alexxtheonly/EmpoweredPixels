using System.Threading.Tasks;
using EmpoweredPixels.Utilities.LeageExecution;

namespace EmpoweredPixels.Jobs
{
  public class LeagueJob : ILeagueJob
  {
    private readonly ILeagueExecutor leagueExecutor;

    public LeagueJob(ILeagueExecutor leagueExecutor)
    {
      this.leagueExecutor = leagueExecutor;
    }

    public async Task RunMatchAsync(int leagueId)
    {
      await leagueExecutor.Execute(leagueId);
    }
  }
}
