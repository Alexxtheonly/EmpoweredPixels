using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Dashboard;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Dashboard
{
  public class DashboardController : ControllerBase<DatabaseContext, DashboardController>
  {
    public DashboardController(DatabaseContext context, ILogger<DashboardController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet("leagues")]
    public async Task<ActionResult<IEnumerable<DashboardLeagueDto>>> GetLeaguePanel()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var dashboardLeagues = await Context.Leagues
        .Select(o => new
        {
          o.Id,
          o.Name,
          IsSubscribed = Context.LeagueSubscriptions.Any(s => s.Fighter.UserId == userId),
        }).ToListAsync();

      var dtos = new List<DashboardLeagueDto>();
      foreach (var dashboardLeague in dashboardLeagues)
      {
        dtos.Add(new DashboardLeagueDto()
        {
          LeagueId = dashboardLeague.Id,
          LeagueName = dashboardLeague.Name,
          IsSubscribed = dashboardLeague.IsSubscribed,
        });
      }

      return Ok(dtos);
    }

    [HttpGet("fighters")]
    public async Task<ActionResult<IEnumerable<DashboardFighterDto>>> GetFighterPanel()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await Context.Fighters
        .Where(o => o.UserId == userId)
        .ProjectTo<DashboardFighterDto>(Mapper.ConfigurationProvider)
        .ToListAsync());
    }

    [HttpGet("results/{fighterId}")]
    public async Task<ActionResult<IEnumerable<DashboardFighterResultDto>>> GetResultPanel(Guid fighterId)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var dtos = await Context.Matches
        .Where(o => Context.MatchRegistrations.Any(r => r.MatchId == o.Id && r.FighterId == fighterId))
        .OrderByDescending(o => o.Started)
        .Take(10)
        .Select(o => new DashboardFighterResultDto()
        {
          LeagueId = Context.LeagueMatches.First(l => l.MatchId == o.Id).LeagueId,
          LeagueName = Context.LeagueMatches.First(l => l.MatchId == o.Id).League.Name,
          MatchId = o.Id,
          MatchStart = o.Started,
          FighterId = fighterId,
          FighterName = Context.Fighters.Where(f => f.Id == fighterId).First().Name,
          IsFirst = Context.MatchContributions.First(c => c.MatchId == o.Id && c.FighterId == fighterId).HasWon,
          IsSecond = Context.MatchContributions.First(c => c.MatchId == o.Id && c.FighterId == fighterId).IsSecond,
          IsThird = Context.MatchContributions.First(c => c.MatchId == o.Id && c.FighterId == fighterId).IsThird,
        })
        .ToListAsync();

      return Ok(dtos);
    }
  }
}
