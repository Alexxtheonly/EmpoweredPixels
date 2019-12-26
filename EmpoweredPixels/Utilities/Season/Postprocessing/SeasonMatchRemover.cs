using System.Threading.Tasks;
using EmpoweredPixels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public class SeasonMatchRemover : ISeasonMatchRemover
  {
    private readonly ILogger<SeasonMatchRemover> logger;

    public SeasonMatchRemover(ILogger<SeasonMatchRemover> logger)
    {
      this.logger = logger;
    }

    public async Task RemoveAsync(DatabaseContext context)
    {
      var matches = await context.Matches.ToListAsync();

      logger.LogWarning($"Deleting {matches.Count} league matches");

      context.RemoveRange(matches);
    }
  }
}
