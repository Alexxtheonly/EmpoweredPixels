using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Leagues;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Utilities.Paging;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpFightingEngine.Fighters;

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

    [HttpGet("{id}")]
    public async Task<ActionResult<LeagueDetailDto>> GetLeague(int id)
    {
      return Ok(await Context.Leagues
        .Include(o => o.Subscriptions)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.User)
        .ProjectTo<LeagueDetailDto>(Mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(o => o.Id == id));
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

      if (fighter.PowerLevel() > league.Options.MatchOptions.MaxPowerlevel)
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

      if (subscription == null)
      {
        return BadRequest();
      }

      Context.Remove(subscription);
      await Context.SaveChangesAsync();

      return Ok();
    }

    [HttpGet("{id}/subscriptions")]
    public async Task<ActionResult<IEnumerable<LeagueSubscriptionDto>>> GetLeagueSubscriptions(int id)
    {
      return Ok(await Context.LeagueSubscriptions
        .Where(o => o.LeagueId == id)
        .Include(o => o.Fighter)
        .ThenInclude(o => o.User)
        .ProjectTo<LeagueSubscriptionDto>(Mapper.ConfigurationProvider)
        .ToListAsync());
    }

    [HttpGet("{id}/subscriptions/user")]
    public async Task<ActionResult<IEnumerable<LeagueSubscriptionDto>>> GetLeagueSubscriptionsForUser(int id)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await Context.LeagueSubscriptions
        .Where(o => o.LeagueId == id)
        .Include(o => o.Fighter)
        .ThenInclude(o => o.User)
        .Where(o => o.Fighter.UserId == userId)
        .ProjectTo<LeagueSubscriptionDto>(Mapper.ConfigurationProvider)
        .ToListAsync());
    }

    [HttpPost("{id}/matches")]
    public async Task<ActionResult<Page<LeagueMatchDto>>> GetMatches(int id, [FromBody] PagingOptions options)
    {
      return Ok(await Context.LeagueMatches
        .Where(o => o.LeagueId == id)
        .Include(o => o.Match)
        .ThenInclude(o => o.MatchFighterResults)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.User)
        .OrderByDescending(o => o.Match.Started)
        .ProjectTo<LeagueMatchDto>(Mapper.ConfigurationProvider)
        .GetPage(options));
    }

    [HttpPost("{id}/highscores")]
    public async Task<ActionResult<IEnumerable<LeagueHighscoreDto>>> GetHighscores(int id, [FromBody] LeagueHighscoreOptionsDto optionsDto)
    {
      var matches = Context.LeagueMatches
        .Where(o => o.LeagueId == id)
        .Include(o => o.Match)
        .OrderByDescending(o => o.Match.Started)
        .Take(optionsDto.LastMatches);

      var scores = await Context.MatchFighterResults
        .Where(o => matches.Any(m => m.MatchId == o.MatchId))
        .Where(o => o.Result == Enums.Matches.Result.Win)
        .Include(o => o.Fighter.User)
        .GroupBy(o => o.Fighter)
        .ToListAsync();

      return Ok(scores
        .Select(o => new LeagueHighscoreDto()
        {
          FighterId = o.Key.Id,
          FighterName = o.Key.Name,
          Username = o.Key.User.Name,
          Score = o.Count(),
        })
        .OrderByDescending(o => o.Score)
        .Take(5));
    }
  }
}
