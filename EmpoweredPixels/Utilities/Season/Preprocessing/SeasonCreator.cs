using System;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.Season.Preprocessing
{
  public class SeasonCreator : ISeasonCreator
  {
    private static readonly Random Random = new Random();

    private readonly TimeSpan length = TimeSpan.FromDays(365.25 / 12);

    private readonly DatabaseContext context;
    private readonly IDateTimeProvider dateTimeProvider;

    public SeasonCreator(DatabaseContext context, IDateTimeProvider dateTimeProvider)
    {
      this.context = context;
      this.dateTimeProvider = dateTimeProvider;
    }

    public async Task<Models.Seasons.Season> CreateAsync()
    {
      var lastSeasons = await context.Seasons
        .OrderByDescending(o => o.EndDate)
        .Take(SeasonConstants.Seasons.Count)
        .Select(o => o.SeasonId)
        .ToListAsync();

      var possibleSeasons = SeasonConstants.Seasons.Except(lastSeasons);
      if (!possibleSeasons.Any())
      {
        possibleSeasons = SeasonConstants.Seasons;
      }

      var now = dateTimeProvider.Now;

      var season = new Models.Seasons.Season()
      {
        StartDate = now,
        EndDate = now.Add(length),
        SeasonId = possibleSeasons.Random(Random),
      };

      return season;
    }
  }
}
