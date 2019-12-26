using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.MatchExecution;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.LeageExecution
{
  public class LeagueExecutor : ILeagueExecutor
  {
    private readonly DatabaseContext databaseContext;
    private readonly IMatchExecutor matchExecutor;
    private readonly ILeagueDivisionDivider divisionDivider;
    private readonly IDateTimeProvider dateTimeProvider;

    public LeagueExecutor(
      DatabaseContext databaseContext,
      IMatchExecutor matchExecutor,
      ILeagueDivisionDivider divisionDivider,
      IDateTimeProvider dateTimeProvider)
    {
      this.databaseContext = databaseContext;
      this.matchExecutor = matchExecutor;
      this.divisionDivider = divisionDivider;
      this.dateTimeProvider = dateTimeProvider;
    }

    public async Task Execute(int leagueId)
    {
      var league = await GetLeague(leagueId);

      var fighters = await databaseContext.Fighters.Include(o => o.EloRating).ToListAsync();

      var divisions = divisionDivider.GetDivisions(fighters);

      foreach (var division in divisions)
      {
        await ExecuteDivisionMatch(league, division);
      }
    }

    private async Task ExecuteDivisionMatch(League league, IGrouping<int, Fighter> division)
    {
      var match = new Match()
      {
        Created = dateTimeProvider.Now,
        Options = league.Options.MatchOptions,
      };

      AddMatchRegistrations(division, match);

      databaseContext.Add(match);
      await databaseContext.SaveChangesAsync();

      match = await GetMatch(match);

      databaseContext.Add(new LeagueMatch()
      {
        LeagueId = league.Id,
        MatchId = match.Id,
        Division = division.Key,
      });

      await matchExecutor.Execute(match);
      await databaseContext.SaveChangesAsync();
    }

    private Task<Match> GetMatch(Match match)
    {
      return databaseContext.Matches
        .AsTracking()
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter.Configuration)
        .FirstOrDefaultAsync(o => o.Id == match.Id);
    }

    private void AddMatchRegistrations(IGrouping<int, Fighter> division, Match match)
    {
      foreach (var subscriber in division)
      {
        match.Registrations.Add(new MatchRegistration()
        {
          Date = dateTimeProvider.Now,
          FighterId = subscriber.Id,
          Match = match,
        });
      }
    }

    private Task<League> GetLeague(int leagueId)
    {
      return databaseContext.Leagues
        .Where(o => !o.IsDeactivated)
        .Include(o => o.Subscriptions)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.EloRating)
        .Include(o => o.Subscriptions)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == leagueId);
    }
  }
}
