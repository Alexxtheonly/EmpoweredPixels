using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.DataTransferObjects.Roster;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Exceptions.Roster;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Utilities.EquipmentGeneration;
using EmpoweredPixels.Utilities.FighterEquipment;
using EmpoweredPixels.Utilities.FighterProgress;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Roster
{
  public class FighterController : ControllerBase<DatabaseContext, FighterController>
  {
    private readonly IDateTimeProvider dateTimeProvider;

    public FighterController(DatabaseContext context, ILogger<FighterController> logger, IMapper mapper, IDateTimeProvider dateTimeProvider)
      : base(context, logger, mapper)
    {
      this.dateTimeProvider = dateTimeProvider;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FighterDto>>> GetFighters()
    {
      var userId = User.Claims.GetUserId();

      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await Context.Fighters
        .Where(o => o.UserId == userId)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.Option)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .ProjectTo<FighterDto>(Mapper.ConfigurationProvider)
        .ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FighterDto>> GetFighter(Guid id)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.Option)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == id);
      if (fighter == null)
      {
        return NotFound();
      }

      return Ok(Mapper.Map<FighterDto>(fighter));
    }

    [HttpGet("{id}/name")]
    public async Task<ActionResult<FighterNameDto>> GetFighterName(Guid id)
    {
      return Ok(await Context.Fighters
        .Where(o => o.Id == id)
        .ProjectTo<FighterNameDto>(Mapper.ConfigurationProvider)
        .FirstOrDefaultAsync());
    }

    [HttpPut]
    public async Task<ActionResult<FighterDto>> CreateFighter(
      [FromBody] FighterDto dto,
      [FromServices] IFighterLevelUpHandler fighterLevelUpHandler,
      [FromServices] IFighterOutfitter fighterOutfitter,
      [FromServices] IEquipmentGenerator equipmentGenerator)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var name = dto.Name;

      if (string.IsNullOrEmpty(name) || await Context.Fighters.AnyAsync(o => o.Name == name))
      {
        return BadRequest();
      }

      if (await Context.Fighters.AnyAsync(o => o.UserId == userId))
      {
        return BadRequest();
      }

      var fighter = new Fighter()
      {
        Name = name,
        UserId = userId.Value,
        Created = dateTimeProvider.Now,
        Equipment = new List<Equipment>(),
      };

      fighterLevelUpHandler.Up(fighter);

      var armorHead = equipmentGenerator.GenerateArmorHead(fighter.Level, ItemRarity.Basic, userId.Value);
      var armorShoulders = equipmentGenerator.GenerateArmorShoulders(fighter.Level, ItemRarity.Basic, userId.Value);
      var armorChest = equipmentGenerator.GenerateArmorChest(fighter.Level, ItemRarity.Basic, userId.Value);
      var armorHands = equipmentGenerator.GenerateArmorHands(fighter.Level, ItemRarity.Basic, userId.Value);
      var armorLegs = equipmentGenerator.GenerateArmorLegs(fighter.Level, ItemRarity.Basic, userId.Value);
      var armorShoes = equipmentGenerator.GenerateArmorShoes(fighter.Level, ItemRarity.Basic, userId.Value);

      var weapon = equipmentGenerator.GenerateWeaponGreatsword(fighter.Level, ItemRarity.Basic, userId.Value);

      fighterOutfitter.Equip(fighter, armorHead, false);
      fighterOutfitter.Equip(fighter, armorShoulders, false);
      fighterOutfitter.Equip(fighter, armorChest, false);
      fighterOutfitter.Equip(fighter, armorHands, false);
      fighterOutfitter.Equip(fighter, armorLegs, false);
      fighterOutfitter.Equip(fighter, armorShoes, false);

      fighterOutfitter.Equip(fighter, weapon, false);

      Context.Fighters.Add(fighter);
      Context.Equipment.AddRange(armorHead, armorShoulders, armorChest, armorHands, armorLegs, armorShoes, weapon);
      await Context.SaveChangesAsync();

      return Mapper.Map<FighterDto>(fighter);
    }

    [HttpPost("{id}/equip")]
    public async Task<ActionResult<FighterDto>> Equip(Guid id, [FromBody] EquipmentDto dto, [FromServices] IFighterOutfitter fighterOutfitter)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var fighter = await Context.Fighters
        .AsTracking()
        .Where(o => o.UserId == userId)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.Option)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == id);

      if (fighter == null)
      {
        return BadRequest(new InvalidFighterException());
      }

      var equipment = await Context.Equipment
        .AsTracking()
        .Where(o => o.UserId == userId)
        .Include(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == dto.Id);

      if (equipment == null)
      {
        return BadRequest();
      }

      fighterOutfitter.Equip(fighter, equipment, true);

      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<FighterDto>(fighter));
    }

    [HttpPost("{id}/unequip")]
    public async Task<ActionResult<FighterDto>> Unequip(Guid id, [FromBody] EquipmentDto dto, [FromServices] IFighterOutfitter fighterOutfitter)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var fighter = await Context.Fighters
        .AsTracking()
        .Where(o => o.UserId == userId)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.Option)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == id);

      if (fighter == null)
      {
        return BadRequest(new InvalidFighterException());
      }

      var equipment = await Context.Equipment
        .AsTracking()
        .Where(o => o.UserId == userId)
        .Include(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == dto.Id);

      if (equipment == null)
      {
        return BadRequest();
      }

      fighterOutfitter.Unequip(fighter, equipment);

      await Context.SaveChangesAsync();

      return Mapper.Map<FighterDto>(fighter);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFighter(Guid id)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var fighter = await Context.Fighters
        .AsTracking()
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == id);

      if (fighter == null)
      {
        return BadRequest(new InvalidFighterException());
      }

      fighter.IsDeleted = true;

      var subscriptions = await Context.LeagueSubscriptions
        .Where(o => o.FighterId == fighter.Id)
        .ToListAsync();

      Context.LeagueSubscriptions.RemoveRange(subscriptions);
      await Context.SaveChangesAsync();

      return Ok();
    }

    [HttpGet("{id}/experience")]
    public async Task<ActionResult<FighterExperienceDto>> GetExperience(Guid id, [FromServices] IFighterExperienceCalculator fighterExperienceCalculator)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var fighterExperience = await Context.FighterExperiences.FirstOrDefaultAsync(o => o.FighterId == id);
      if (fighterExperience == null)
      {
        return Ok(new FighterExperienceDto()
        {
          Level = 1,
          CurrentExp = 0,
          LevelExp = fighterExperienceCalculator.GetNeededExperience(1),
        });
      }

      var level = fighterExperienceCalculator.GetLevel(fighterExperience);

      return Ok(new FighterExperienceDto()
      {
        Level = level.Level,
        CurrentExp = level.Experience,
        LevelExp = level.RequiredExperience,
      });
    }

    [HttpGet("{id}/configuration")]
    public async Task<ActionResult<FighterConfigurationDto>> GetFighterConfiguration(Guid id)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == id);

      if (fighter == null)
      {
        return BadRequest();
      }

      var config = await Context.FighterConfigurations
        .ProjectTo<FighterConfigurationDto>(Mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(o => o.FighterId == id);

      if (config == null)
      {
        config = new FighterConfigurationDto()
        {
          FighterId = id,
        };
      }

      return Ok(config);
    }

    [HttpPost("{id}/configuration")]
    public async Task<ActionResult<FighterConfigurationDto>> UpdateFighterConfiguration(Guid id, [FromBody] FighterConfigurationDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      if (id != dto.FighterId)
      {
        return BadRequest();
      }

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == id);

      if (fighter == null)
      {
        return BadRequest();
      }

      var config = await Context.FighterConfigurations
        .AsTracking()
        .FirstOrDefaultAsync(o => o.FighterId == id);

      if (config == null)
      {
        config = new FighterConfiguration();
        Context.FighterConfigurations.Add(config);
      }

      if (config.AttunementId != dto.AttunementId)
      {
        var particles = await Context.Items
          .Where(o => o.UserId == userId)
          .Where(o => o.ItemId == EmpoweredParticle.Id)
          .Take(5000)
          .ToListAsync();

        if (particles.Count != 5000)
        {
          return BadRequest();
        }

        Context.RemoveRange(particles);
      }

      Mapper.Map(dto, config);

      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<FighterConfigurationDto>(config));
    }
  }
}
