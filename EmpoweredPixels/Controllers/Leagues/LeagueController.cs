using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Leagues;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Leagues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Leagues
{
  public class LeagueController : ControllerBase<DatabaseContext, LeagueController>
  {
    public LeagueController(DatabaseContext context, ILogger<LeagueController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LeagueDto>>> GetLeagues()
    {
      return Ok(await Context.Leagues
        .Include(o => o.Subscriptions)
        .ProjectTo<LeagueDto>(Mapper.ConfigurationProvider)
        .ToListAsync());
    }

    [HttpPost("subscribe")]
    public async Task<ActionResult> Subscribe([FromBody] LeagueSubscriptionDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var league = await Context.Leagues
        .Where(o => !o.IsDeactivated)
        .FirstOrDefaultAsync(o => o.Id == dto.LeagueId);

      if (league == null)
      {
        return BadRequest();
      }

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == dto.FighterId);

      if (fighter == null)
      {
        return BadRequest();
      }

      var subscriptions = await Context.LeagueSubscriptions
        .Include(o => o.Fighter)
        .Where(o => o.LeagueId == league.Id && o.Fighter.UserId == userId)
        .ToListAsync();

      if ((subscriptions.Count + 1) > league.Options.MatchOptions.MaxFightersPerUser)
      {
        return BadRequest();
      }

      Context.Add(Mapper.Map<LeagueSubscription>(dto));
      await Context.SaveChangesAsync();

      return Ok();
    }

    [HttpPost("unsubscribe")]
    public async Task<ActionResult> Unsubscribe([FromBody] LeagueSubscriptionDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var league = await Context.Leagues
        .Where(o => !o.IsDeactivated)
        .FirstOrDefaultAsync(o => o.Id == dto.LeagueId);

      if (league == null)
      {
        return BadRequest();
      }

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == dto.FighterId);

      if (fighter == null)
      {
        return BadRequest();
      }

      var subscription = await Context.LeagueSubscriptions
        .FirstOrDefaultAsync(o => o.FighterId == fighter.Id && o.LeagueId == league.Id);

      Context.Remove(subscription);
      await Context.SaveChangesAsync();

      return Ok();
    }

    [HttpGet("{id}/matches")]
    public async Task<ActionResult<IEnumerable<LeagueMatchDto>>> GetMatches(int id)
    {
      return Ok(await Context.LeagueMatches
        .Where(o => o.LeagueId == id)
        .Include(o => o.Match)
        .ProjectTo<LeagueMatchDto>(Mapper.ConfigurationProvider)
        .ToListAsync());
    }
  }
}
