using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Seasons;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.Season.Postprocessing;
using EmpoweredPixels.Utilities.Season.Preprocessing;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Jobs.Seasons
{
  public class SeasonInitiatorJob : ISeasonInitiatorJob
  {
    private readonly DatabaseContext context;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly ISeasonCreator seasonCreator;
    private readonly IBackgroundJobClient backgroundJobClient;
    private readonly ISeasonMatchRemover seasonMatchRemover;

    public SeasonInitiatorJob(
      DatabaseContext context,
      IDateTimeProvider dateTimeProvider,
      ISeasonCreator seasonCreator,
      IBackgroundJobClient backgroundJobClient,
      ISeasonMatchRemover seasonMatchRemover)
    {
      this.context = context;
      this.dateTimeProvider = dateTimeProvider;
      this.seasonCreator = seasonCreator;
      this.backgroundJobClient = backgroundJobClient;
      this.seasonMatchRemover = seasonMatchRemover;
    }

    public async Task InitAsync()
    {
      var currentSeason = await context.Seasons.OrderByDescending(o => o.EndDate).FirstOrDefaultAsync();

      if (currentSeason.EndDate > dateTimeProvider.Now)
      {
        return;
      }

      var progresses = await context.Users
        .Select(o => new SeasonProgress()
        {
          SeasonId = currentSeason.Id,
          UserId = o.Id,
          Position = context.FighterEloRatings
          .Where(r => r.CurrentElo > (context.FighterEloRatings.Where(e => e.Fighter.UserId == o.Id).FirstOrDefault() == null ? 0 : context.FighterEloRatings.Where(e => e.Fighter.UserId == o.Id).OrderByDescending(e => e.CurrentElo).FirstOrDefault().CurrentElo)).Count(),
        }).ToListAsync();

      await seasonMatchRemover.RemoveAsync(context);

      context.AddRange(progresses);
      context.Add(await seasonCreator.CreateAsync());
      await context.SaveChangesAsync();

      foreach (var progress in progresses)
      {
        backgroundJobClient.Enqueue<ISeasonUserJob>(o => o.ExecuteAsync(progress.SeasonId, progress.UserId));
      }
    }
  }
}
