using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Roster;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
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
        .SingleOrDefaultAsync();
      if (fighter == null)
      {
        return NotFound();
      }

      return Mapper.Map<FighterDto>(fighter);
    }

    [HttpPut]
    public async Task<ActionResult<FighterDto>> CreateFighter([FromBody] string name)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var fighter = new Fighter()
      {
        Name = name,
        UserId = userId.Value,
        Created = dateTimeProvider.Now,
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
  }
}
