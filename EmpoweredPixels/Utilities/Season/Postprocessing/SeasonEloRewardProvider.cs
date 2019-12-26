using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Seasons;
using EmpoweredPixels.Rewards.Items;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public class SeasonEloRewardProvider : ISeasonEloRewardProvider
  {
    private const float Factor = 20;

    public SeasonEloRewardProvider()
    {
    }

    public async Task ProvideAsync(DatabaseContext context, SeasonSummary summary)
    {
      var fighterElo = await context.FighterEloRatings
        .Where(o => o.Fighter.UserId == summary.UserId)
        .Include(o => o.Fighter)
        .FirstOrDefaultAsync();

      if (fighterElo == null)
      {
        return;
      }

      var rewardCount = (int)(fighterElo.CurrentElo * Factor);

      summary.Bonus = rewardCount;

      for (int rewardCounter = 0; rewardCounter < rewardCount; rewardCounter++)
      {
        context.Add(new Item()
        {
          ItemId = EmpoweredParticle.Id,
          Rarity = Enums.Equipment.ItemRarity.Basic,
          UserId = fighterElo.Fighter.UserId,
        });
      }

      context.Remove(fighterElo);
    }
  }
}
