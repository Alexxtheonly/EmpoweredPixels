using System;
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

    [HttpGet("balance/particles")]
    public async Task<ActionResult<CurrencyBalanceDto>> GetParticleBalance()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await GetBalance(userId, EmpoweredParticle.Id));
    }

    [HttpGet("balance/token/common")]
    public async Task<ActionResult<CurrencyBalanceDto>> GetTokenCommonBalance()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await GetBalance(userId, EquipmentToken.Common));
    }

    [HttpGet("balance/token/rare")]
    public async Task<ActionResult<CurrencyBalanceDto>> GetTokenRareBalance()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await GetBalance(userId, EquipmentToken.Rare));
    }

    [HttpGet("balance/token/fabled")]
    public async Task<ActionResult<CurrencyBalanceDto>> GetTokenFabledBalance()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await GetBalance(userId, EquipmentToken.Fabled));
    }

    [HttpGet("balance/token/mythic")]
    public async Task<ActionResult<CurrencyBalanceDto>> GetTokenMythicBalance()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await GetBalance(userId, EquipmentToken.Mythic));
    }

    private async Task<CurrencyBalanceDto> GetBalance(long? userId, Guid item)
    {
      var balance = await Context.Items
        .Where(o => o.UserId == userId)
        .Where(o => o.ItemId == item)
        .CountAsync();

      return new CurrencyBalanceDto()
      {
        Balance = balance,
        ItemId = item,
      };
    }
  }
}
