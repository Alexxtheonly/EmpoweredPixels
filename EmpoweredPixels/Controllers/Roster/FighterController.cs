using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Roster;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpFightingEngine.Fighters;

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
    public async Task<ActionResult<FighterDto>> CreateFighter([FromBody] FighterDto dto)
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

      var fighter = new Fighter()
      {
        Name = name,
        UserId = userId.Value,
        Created = dateTimeProvider.Now,
        Accuracy = 1,
        Agility = 1,
        Expertise = 1,
        Power = 1,
        Regeneration = 1,
        Speed = 1,
        Stamina = 1,
        Toughness = 1,
        Vision = 1,
        Vitality = 1,
      };

      // todo: set max fighters per user
      Context.Fighters.Add(fighter);
      await Context.SaveChangesAsync();

      return Mapper.Map<FighterDto>(fighter);
    }

    [HttpPost]
    public async Task<ActionResult<FighterDto>> UpdateFighter([FromBody] FighterDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null || dto.UserId != userId)
      {
        return Forbid();
      }

      var fighter = await Context.Fighters
        .AsTracking()
        .SingleOrDefaultAsync(o => o.Id == dto.Id && o.UserId == userId);
      if (fighter == null)
      {
        return NotFound();
      }

      Mapper.Map(dto, fighter);

      await Context.SaveChangesAsync();

      return Mapper.Map<FighterDto>(fighter);
    }

    [HttpPost("forecast")]
    public ActionResult<FighterStatForecastDto> GetForecast([FromBody] FighterDto dto, [FromServices]IEngineFactory engineFactory)
    {
      IFighterStats fighter = new GenericFighter()
      {
        Accuracy = dto.Accuracy,
        Agility = dto.Agility,
        Expertise = dto.Expertise,
        Power = dto.Power,
        Regeneration = dto.Regeneration,
        Speed = dto.Speed,
        Stamina = dto.Stamina,
        Toughness = dto.Toughness,
        Vision = dto.Vision,
        Vitality = dto.Vitality,
      };

      var calculationValues = engineFactory.CalculationValues;
      var forecast = new FighterStatForecastDto()
      {
        CritChance = fighter.CriticalHitChance(calculationValues),
        DodgeChance = fighter.DodgeChance(calculationValues),
        Energy = fighter.Energy(calculationValues),
        EnergyRegeneration = fighter.EnergyRegeneration(calculationValues),
        Health = fighter.Health(calculationValues),
        HealthRegeneration = fighter.HealthRegeneration(calculationValues),
        Velocity = fighter.Velocity(calculationValues),
        Vision = fighter.VisualRange(calculationValues),
        MissChance = fighter.HitChance(calculationValues),
        PotentialDefense = fighter.PotentialDefense(calculationValues),
        PotentialPower = fighter.PotentialPower(calculationValues),
      };

      return Ok(forecast);
    }
  }
}
