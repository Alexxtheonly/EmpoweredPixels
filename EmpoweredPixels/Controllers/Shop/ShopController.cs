using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.DataTransferObjects.Rewards;
using EmpoweredPixels.DataTransferObjects.Shop;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.EquipmentGeneration;
using EmpoweredPixels.Utilities.Paging;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Shop
{
  public class ShopController : ControllerBase<DatabaseContext, ShopController>
  {
    public ShopController(DatabaseContext context, ILogger<ShopController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpPost]
    public ActionResult<Page<ShopItemDto>> GetShopContent([FromBody] ShopFilterDto filterDto)
    {
      var items = ShopInventory.Items;

      if (filterDto.CategoryId != null)
      {
        items = items.Where(o => o.CategoryId == filterDto.CategoryId);
      }

      if (filterDto.Rarity != null)
      {
        items = items.Where(o => o.Rarity == filterDto.Rarity);
      }

      return Ok(items
        .OrderByDescending(o => o.Rarity)
        .GetPage(filterDto));
    }

    [HttpPost("buy")]
    public async Task<ActionResult<RewardContentDto>> Buy([FromBody] ShopItemDto dto, [FromServices] IEquipmentGenerator equipmentGenerator)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var item = ShopInventory.Items.FirstOrDefault(o => o.ItemId == dto.ItemId && o.Rarity == dto.Rarity);
      if (item == null)
      {
        return BadRequest();
      }

      foreach (var price in item.Prices)
      {
        var currencies = await Context.Items
          .Where(o => o.UserId == userId)
          .Where(o => o.ItemId == price.CurrencyItemId)
          .Take(price.Quantity)
          .ToListAsync();

        if (currencies.Count != price.Quantity)
        {
          return BadRequest();
        }

        Context.RemoveRange(currencies);
      }

      var content = new RewardContentDto();

      if (EquipmentConstants.IsEquipmentConstant(item.ItemId))
      {
        var fighterLevel = await Context.Fighters
          .Where(o => o.UserId == userId)
          .MaxAsync(o => o.Level);

        var itemLevel = fighterLevel.NearestBase(8);
        if (itemLevel < 1)
        {
          itemLevel = 1;
        }

        var equipment = equipmentGenerator.GenerateEquipment(item.ItemId, itemLevel, item.Rarity, userId.Value);
        Context.Add(equipment);
        content.Equipment.Add(Mapper.Map<EquipmentDto>(equipment));
      }
      else
      {
        var boughtItem = new Item()
        {
          ItemId = item.ItemId,
          UserId = userId.Value,
        };

        Context.Items.Add(boughtItem);
        content.Items.Add(Mapper.Map<ItemDto>(boughtItem));
      }

      await Context.SaveChangesAsync();

      return Ok(content);
    }
  }
}
