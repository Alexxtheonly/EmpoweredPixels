using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Utilities.LeageExecution;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Jobs
{
  public class LeagueJob : ILeagueJob
  {
    private readonly ILeagueExecutor leagueExecutor;
    private readonly DatabaseContext context;

    public LeagueJob(DatabaseContext context, ILeagueExecutor leagueExecutor)
    {
      this.context = context;
      this.leagueExecutor = leagueExecutor;
    }

    public async Task RunMatchAsync(int leagueId)
    {
      if (await context.SeasonProgresses.AnyAsync(o => !o.IsComplete))
      {
        return;
      }

      await leagueExecutor.Execute(leagueId);
    }
  }
}
