using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Inventory;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Rewards.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Inventory
{
  public class InventoryController : ControllerBase<DatabaseContext, InventoryController>
  {
    public InventoryController(DatabaseContext context, ILogger<InventoryController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet("balance")]
    public async Task<ActionResult<CurrencyBalanceDto>> GetBalance()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var balance = await Context.Items
        .Where(o => o.UserId == userId)
        .Where(o => o.ItemId == EmpoweredParticle.Id)
        .CountAsync();

      return Ok(new CurrencyBalanceDto()
      {
        Balance = balance,
      });
    }
  }
}
