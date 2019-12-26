using System;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Seasons;
using EmpoweredPixels.Rewards.Items;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public class SeasonPostprocessor : ISeasonPostprocessor
  {
    private readonly ISeasonMatchRemover matchRemover;
    private readonly ISeasonFighterResetter seasonFighterResetter;
    private readonly ISeasonSalvageRewardProvider seasonSalvageRewardProvider;
    private readonly ISeasonEloRewardProvider seasonEloRewardProvider;
    private readonly ISeasonRewardClaimer seasonRewardClaimer;

    public SeasonPostprocessor(
      ISeasonFighterResetter seasonFighterResetter,
      ISeasonSalvageRewardProvider seasonSalvageRewardProvider,
      ISeasonEloRewardProvider seasonEloRewardProvider,
      ISeasonRewardClaimer seasonRewardClaimer)
    {
      this.seasonFighterResetter = seasonFighterResetter;
      this.seasonSalvageRewardProvider = seasonSalvageRewardProvider;
      this.seasonEloRewardProvider = seasonEloRewardProvider;
      this.seasonRewardClaimer = seasonRewardClaimer;
    }

    public async Task ProcessAsync(DatabaseContext context, SeasonProgress seasonProgress)
    {
      var season = await context.Seasons
        .OrderByDescending(o => o.EndDate)
        .FirstOrDefaultAsync();

      if (season == null)
      {
        throw new ArgumentNullException("no active season found");
      }

      await seasonRewardClaimer.ClaimAsync(context, seasonProgress.UserId);
      await context.SaveChangesAsync();

      var summary = new SeasonSummary()
      {
        SeasonId = seasonProgress.SeasonId,
        UserId = seasonProgress.UserId,
        Position = seasonProgress.Position + 1,
      };
      context.Add(summary);

      await seasonSalvageRewardProvider.ProvideAsync(context, summary);
      await seasonEloRewardProvider.ProvideAsync(context, summary);
      await seasonFighterResetter.ResetAsync(context, seasonProgress.UserId);

      var tokens = await context.Items.Where(o => EquipmentToken.Tokens.Contains(o.ItemId)).ToListAsync();

      context.RemoveRange(tokens);
      context.RemoveRange(await context.FighterExperiences.ToListAsync());
    }
  }
}
