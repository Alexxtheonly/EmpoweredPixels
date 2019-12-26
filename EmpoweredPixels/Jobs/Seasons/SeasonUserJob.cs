using System;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Utilities.Season.Postprocessing;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Jobs.Seasons
{
  public class SeasonUserJob : ISeasonUserJob
  {
    private readonly DatabaseContext context;
    private readonly ISeasonPostprocessor seasonPostprocessor;

    public SeasonUserJob(DatabaseContext context, ISeasonPostprocessor seasonPostprocessor)
    {
      this.context = context;
      this.seasonPostprocessor = seasonPostprocessor;
    }

    public async Task ExecuteAsync(int seasonId, long userId)
    {
      var progress = await context.SeasonProgresses
        .Where(o => o.SeasonId == seasonId && o.UserId == userId)
        .AsTracking()
        .FirstOrDefaultAsync();

      if (progress == null)
      {
        throw new ArgumentNullException($"No season progress found for season:{seasonId} user:{userId}");
      }

      await seasonPostprocessor.ProcessAsync(context, progress);

      progress.IsComplete = true;

      await context.SaveChangesAsync();
    }
  }
}
