using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models;
using EmpoweredPixels.Rewards.Items;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Jobs.Inventory
{
  public class RemoveExcessParticlesJob : IRemoveExcessParticlesJob
  {
    private const int MaxParticles = 2500;

    private readonly DatabaseContext context;

    public RemoveExcessParticlesJob(DatabaseContext context)
    {
      this.context = context;
    }

    public async Task RemoveAsync()
    {
      var groups = await context.Items
        .Where(o => o.ItemId == EmpoweredParticle.Id)
        .GroupBy(o => o.UserId)
        .ToListAsync();

      foreach (var group in groups)
      {
        context.RemoveRange(group.Skip(MaxParticles));
      }

      await context.SaveChangesAsync();
    }
  }
}
