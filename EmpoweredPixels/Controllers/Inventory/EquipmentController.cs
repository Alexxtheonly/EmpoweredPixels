using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.FighterEquipment;
using EmpoweredPixels.Utilities.Paging;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Inventory
{
  public class EquipmentController : ControllerBase<DatabaseContext, EquipmentController>
  {
    public EquipmentController(DatabaseContext context, ILogger<EquipmentController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EquipmentDto>> GetEquipment(Guid id)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var equipment = await Context.Equipment
        .Where(o => o.UserId == userId)
        .Include(o => o.Option)
        .FirstOrDefaultAsync(o => o.Id == id);

      if (equipment == null)
      {
        return BadRequest();
      }

      return Ok(Mapper.Map<EquipmentDto>(equipment));
    }

    [HttpGet("enhance/cost")]
    public ActionResult<int> GetEnhancementCosts([FromServices]IEquipmentEnhancer equipmentEnhancer)
    {
      return Ok(equipmentEnhancer.RequiredParticles);
    }

    [HttpPost("enhance")]
    public async Task<ActionResult<EquipmentDto>> EnhanceEquipment([FromBody]EquipmentDto dto, [FromServices]IEquipmentEnhancer equipmentEnhancer)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var equipment = await Context.Equipment
        .AsTracking()
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == dto.Id);

      if (equipment == null)
      {
        return BadRequest();
      }

      var particles = await Context.Items
        .Where(o => o.UserId == userId && o.ItemId == EmpoweredParticle.Id)
        .Take(equipmentEnhancer.RequiredParticles)
        .ToListAsync();

      equipmentEnhancer.Enhance(equipment, particles);

      Context.RemoveRange(particles);
      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<EquipmentDto>(equipment));
    }

    [HttpPost("salvage")]
    public async Task<ActionResult<IEnumerable<ItemDto>>> SalvageEquipment([FromBody] EquipmentDto dto, [FromServices] IEquipmentSalvager equipmentSalvager)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var equipment = await Context.Equipment
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == dto.Id);

      if (equipment == null)
      {
        return BadRequest();
      }

      var items = equipmentSalvager.Salvage(equipment, userId.Value).ToList();
      Context.Remove(equipment);
      Context.AddRange(items);
      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<IEnumerable<ItemDto>>(items));
    }

    [HttpPost("salvage/inventory")]
    public async Task<ActionResult<IEnumerable<ItemDto>>> SalvageInventory([FromServices] IEquipmentSalvager equipmentSalvager)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var equipment = await Context.Equipment
        .Include(o => o.Option)
        .Where(o => o.FighterId == null && o.UserId == userId)
        .Where(o => o.Option == null || !o.Option.IsFavorite)
        .ToListAsync();

      var items = new List<Item>();
      foreach (var equip in equipment)
      {
        items.AddRange(equipmentSalvager.Salvage(equip, userId.Value).ToList());
        Context.Remove(equip);
      }

      Context.AddRange(items);
      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<IEnumerable<ItemDto>>(items));
    }

    [HttpPost("inventory")]
    public async Task<ActionResult<Page<EquipmentDto>>> GetInventoryPage([FromBody]PagingOptions options)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var equipment = await Context.Equipment
        .Where(o => o.UserId == userId && o.FighterId == null)
        .Include(o => o.Option)
        .Include(o => o.SocketStones)
        .OrderByDescending(o => o.Level)
        .ThenByDescending(o => o.Rarity)
        .ProjectTo<EquipmentDto>(Mapper.ConfigurationProvider)
        .GetPage(options);

      return Ok(equipment);
    }

    [HttpPost("{id}/favorite")]
    public async Task<ActionResult<EquipmentDto>> EquipmentSetFavorite(Guid id)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var equipment = await GetEquipment(id, userId);

      equipment.Option.IsFavorite = true;

      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<EquipmentDto>(equipment));
    }

    [HttpDelete("{id}/favorite")]
    public async Task<ActionResult<EquipmentDto>> EquipmentUnsetFavorite(Guid id)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var equipment = await GetEquipment(id, userId);

      equipment.Option.IsFavorite = false;

      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<EquipmentDto>(equipment));
    }

    private async Task<Equipment> GetEquipment(Guid id, long? userId)
    {
      var equipment = await Context.Equipment
        .Where(o => o.UserId == userId)
        .Include(o => o.Option)
        .AsTracking()
        .FirstOrDefaultAsync(o => o.Id == id);

      if (equipment == null)
      {
        return null;
      }

      if (equipment.Option == null)
      {
        equipment.Option = new EquipmentOption();
      }

      return equipment;
    }
  }
}
