using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Seasons;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.FighterEquipment;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.Season.Postprocessing
{
  public class SeasonSalvageRewardProvider : ISeasonSalvageRewardProvider
  {
    private readonly IEquipmentSalvager equipmentSalvager;

    public SeasonSalvageRewardProvider(IEquipmentSalvager equipmentSalvager)
    {
      this.equipmentSalvager = equipmentSalvager;
    }

    public async Task ProvideAsync(DatabaseContext context, SeasonSummary summary)
    {
      var equipment = await context.Equipment
        .Where(o => o.UserId == summary.UserId)
        .AsTracking()
        .ToListAsync();

      var salvageItems = new List<Item>();
      foreach (var equip in equipment)
      {
        salvageItems.AddRange(equipmentSalvager.Salvage(equip, summary.UserId).Where(o => !EquipmentToken.Tokens.Contains(o.ItemId)));
      }

      summary.SalvageValue = salvageItems.Count;

      var alreadyTracked = context.ChangeTracker.Entries<Equipment>().Where(o => equipment.Any(e => e.Id == o.Entity.Id)).ToList();

      context.RemoveRange(equipment);
      context.AddRange(salvageItems);
    }
  }
}
