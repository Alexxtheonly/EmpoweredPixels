using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Jobs
{
  public class LeagueJob : ILeagueJob
  {
    private readonly DatabaseContext context;
    private readonly IEngineFactory engineFactory;
    private readonly IDateTimeProvider dateTimeProvider;

    public LeagueJob(DatabaseContext context, IEngineFactory engineFactory, IDateTimeProvider dateTimeProvider)
    {
      this.context = context;
      this.engineFactory = engineFactory;
      this.dateTimeProvider = dateTimeProvider;
    }

    public async Task RunMatchAsync(int leagueId)
    {
      var league = await context.Leagues
        .Where(o => !o.IsDeactivated)
        .Include(o => o.Subscriptions)
        .ThenInclude(o => o.Fighter)
        .FirstOrDefaultAsync(o => o.Id == leagueId);

      if (league == null)
      {
        return;
      }

      var match = new Match()
      {
        Created = dateTimeProvider.Now,
        Options = league.Options.MatchOptions,
      };

      foreach (var subscriber in league.Subscriptions)
      {
        match.Registrations.Add(new MatchRegistration()
        {
          Date = dateTimeProvider.Now,
          FighterId = subscriber.FighterId,
          Match = match,
        });
      }

      if (!match.Registrations.Any())
      {
        return;
      }

      context.Add(match);
      await context.SaveChangesAsync();

      match = await context.Matches
        .AsTracking()
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .FirstOrDefaultAsync(o => o.Id == match.Id);

      context.Add(new LeagueMatch()
      {
        LeagueId = league.Id,
        MatchId = match.Id,
      });

      await context.StartMatch(match, dateTimeProvider, engineFactory);

      await context.SaveChangesAsync();
    }
  }
}
