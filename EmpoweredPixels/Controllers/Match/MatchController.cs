using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Hubs.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.Paging;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Controllers.Matches
{
  public class MatchController : ControllerBase<DatabaseContext, MatchController>
  {
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly IHubContext<MatchHub, IMatchClient> matchHubContext;
    private readonly IEngineFactory engineFactory;

    public MatchController(
      DatabaseContext context,
      ILogger<MatchController> logger,
      IMapper mapper,
      IDateTimeProvider dateTimeProvider,
      IHubContext<MatchHub, IMatchClient> matchHubContext,
      IEngineFactory engineFactory)
      : base(context, logger, mapper)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.matchHubContext = matchHubContext;
      this.engineFactory = engineFactory;
    }

    [HttpGet("options/default")]
    public ActionResult<MatchOptionsDto> GetDefaultMatchOptions()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(Mapper.Map<MatchOptionsDto>(engineFactory.GetDefaultOptions()));
    }

    [HttpGet("options/sizes")]
    public ActionResult<IEnumerable<Guid>> GetBattlefieldSizes()
    {
      return Ok(engineFactory.GetAvailableBounds());
    }

    [HttpPut("create")]
    public async Task<ActionResult<MatchDto>> CreateMatchLobby([FromBody]MatchOptionsDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var match = new Match()
      {
        CreatorUserId = userId,
        Created = dateTimeProvider.Now,
        Options = dto,
      };

      Context.Add(match);
      await Context.SaveChangesAsync();

      if (!match.Options.IsPrivate)
      {
        _ = matchHubContext.Clients.All.UpdateMatchBrowser();
      }

      return Ok(Mapper.Map<MatchDto>(match));
    }

    [HttpPut("create/team")]
    public async Task<ActionResult<MatchTeamDto>> CreateTeam([FromBody] MatchTeamOperationDto dto)
    {
      var match = await Context.Matches
        .Where(o => o.Started == null)
        .FirstOrDefaultAsync(o => o.Id == dto.MatchId);

      if (match == null)
      {
        return BadRequest();
      }

      var team = new MatchTeam()
      {
        MatchId = match.Id,
      };

      if (!string.IsNullOrEmpty(dto.Password))
      {
        team.SetPassword(dto.Password);
      }

      Context.Add(team);
      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<MatchTeamDto>(team));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MatchDto>> GetMatch(Guid id)
    {
      var match = await Context.Matches
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.User)
        .FirstOrDefaultAsync(o => o.Id == id);

      return Ok(Mapper.Map<MatchDto>(match));
    }

    [HttpGet("{id}/teams")]
    public async Task<ActionResult<MatchTeamDto>> GetMatchTeams(Guid id)
    {
      return Ok(await Context.MatchTeams
        .Where(o => o.MatchId == id)
        .ProjectTo<MatchTeamDto>(Mapper.ConfigurationProvider)
        .ToListAsync());
    }

    [HttpPost("browse")]
    public async Task<ActionResult<Page<MatchDto>>> GetMatches([FromBody]PagingOptions options)
    {
      return Ok(await Context.Matches
        .Include(o => o.Registrations)
        .Where(o => o.Started == null)
        .Where(o => !o.Options.IsPrivate) // todo: use https://github.com/aspnet/EntityFrameworkCore/issues/11295#issuecomment-373852015
        .ProjectTo<MatchDto>(Mapper.ConfigurationProvider)
        .GetPage(options));
    }

    [HttpPost("join")]
    public async Task<ActionResult> JoinMatch([FromBody] MatchRegistrationDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var match = await Context.Matches
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .Where(o => o.Started == null)
        .FirstOrDefaultAsync(o => o.Id == dto.MatchId);

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == dto.FighterId);

      if (match == null || fighter == null)
      {
        return BadRequest();
      }

      if (match.Options.MaxFightersPerUser != null &&
        match.Registrations
        .Where(o => o.Fighter.UserId == userId)
        .Count() >= match.Options.MaxFightersPerUser)
      {
        return BadRequest();
      }

      if (match.Options.MaxPowerlevel != null &&
        fighter.PowerLevel() > match.Options.MaxPowerlevel)
      {
        return BadRequest();
      }

      var registration = new MatchRegistration()
      {
        MatchId = match.Id,
        FighterId = fighter.Id,
        Date = dateTimeProvider.Now,
      };
      Context.MatchRegistrations.Add(registration);
      await Context.SaveChangesAsync();

      if (!match.Options.IsPrivate)
      {
        _ = matchHubContext.Clients.All.UpdateMatchBrowser();
      }

      await PushMatchUpdate(match.Id);

      return Ok();
    }

    [HttpPost("join/team")]
    public async Task<ActionResult> JoinTeam([FromBody] MatchTeamOperationDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var match = await Context.Matches
        .Where(o => o.Started == null)
        .FirstOrDefaultAsync(o => o.Id == dto.MatchId);

      if (match == null)
      {
        return BadRequest();
      }

      var team = await Context.MatchTeams
        .FirstOrDefaultAsync(o => o.Id == dto.Id);

      if (team == null)
      {
        return BadRequest();
      }

      if (!team.IsValidPassword(dto.Password))
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

      var registration = await Context.MatchRegistrations
        .AsTracking()
        .Include(o => o.Fighter)
        .Where(o => o.Fighter.UserId == userId)
        .Where(o => o.MatchId == match.Id)
        .FirstOrDefaultAsync(o => o.FighterId == dto.FighterId);

      if (registration == null)
      {
        registration = new MatchRegistration()
        {
          MatchId = match.Id,
          FighterId = fighter.Id,
          Date = dateTimeProvider.Now,
        };
        Context.MatchRegistrations.Add(registration);
      }

      if (registration.TeamId == team.Id)
      {
        return Ok();
      }

      registration.TeamId = team.Id;

      await Context.SaveChangesAsync();

      await PushMatchUpdate(match.Id);

      return Ok();
    }

    [HttpPost("leave")]
    public async Task<ActionResult> LeaveMatch([FromBody] MatchRegistrationDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var match = await Context.Matches
        .Where(o => o.Started == null)
        .FirstOrDefaultAsync(o => o.Id == dto.MatchId);

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == dto.FighterId);

      if (match == null || fighter == null)
      {
        return BadRequest();
      }

      var registration = await Context.MatchRegistrations
        .FirstOrDefaultAsync(o => o.FighterId == dto.FighterId && o.MatchId == dto.MatchId);
      if (registration == null)
      {
        return BadRequest();
      }

      Context.MatchRegistrations.Remove(registration);
      await Context.SaveChangesAsync();

      if (!match.Options.IsPrivate)
      {
        _ = matchHubContext.Clients.All.UpdateMatchBrowser();
      }

      await PushMatchUpdate(match.Id);

      return Ok();
    }

    [HttpPost("leave/team")]
    public async Task<ActionResult> LeaveTeam([FromBody] MatchTeamOperationDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var match = await Context.Matches
        .Where(o => o.Started == null)
        .FirstOrDefaultAsync(o => o.Id == dto.MatchId);

      if (match == null)
      {
        return BadRequest();
      }

      var registration = await Context.MatchRegistrations
        .AsTracking()
        .Include(o => o.Fighter)
        .Where(o => o.Fighter.UserId == userId)
        .Where(o => o.MatchId == match.Id)
        .FirstOrDefaultAsync(o => o.FighterId == dto.FighterId);

      if (registration == null)
      {
        return BadRequest();
      }

      if (registration.TeamId == dto.Id)
      {
        return Ok();
      }

      registration.TeamId = null;
      await Context.SaveChangesAsync();

      await PushMatchUpdate(match.Id);

      return Ok();
    }

    [HttpPost("start")]
    public async Task<ActionResult> StartMatch([FromBody] MatchDto dto)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var match = await Context.Matches
        .AsTracking()
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .Where(o => o.CreatorUserId == userId && o.Started == null)
        .FirstOrDefaultAsync(o => o.Id == dto.Id);

      if (match == null || !match.Registrations.Any())
      {
        return BadRequest();
      }

      await StartMatchInternal(match);

      await Context.SaveChangesAsync();

      await PushMatchUpdate(match.Id);

      return Ok();
    }

    [HttpGet("{id}/result")]
    public async Task<ActionResult<MatchResultDto>> GetMatchResult(Guid id)
    {
      var matchResult = await Context.MatchResults
        .FirstOrDefaultAsync(o => o.MatchId == id);

      if (matchResult == null)
      {
        return BadRequest();
      }

      return Content(matchResult.ResultJson.Decompress());
    }

    private async Task PushMatchUpdate(Guid id)
    {
      var match = await Context.Matches
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.User)
        .FirstOrDefaultAsync(o => o.Id == id);

      await matchHubContext.Clients.Group(id.ToString()).UpdateMatch(Mapper.Map<MatchDto>(match));
    }

    private async Task StartMatchInternal(Match match)
    {
      match.Started = dateTimeProvider.Now;

      var fighters = match.Registrations
        .Select(o => new GenericFighter()
        {
          Id = o.Fighter.Id,
          Team = o.TeamId,
          Accuracy = o.Fighter.Accuracy,
          Agility = o.Fighter.Agility,
          Expertise = o.Fighter.Expertise,
          Power = o.Fighter.Power,
          Regeneration = o.Fighter.Regeneration,
          Speed = o.Fighter.Speed,
          Stamina = o.Fighter.Stamina,
          Toughness = o.Fighter.Toughness,
          Vision = o.Fighter.Vision,
          Vitality = o.Fighter.Vitality,
        });

      var engine = engineFactory.GetEngine(fighters, match.Options);
      var result = engine.StartMatch();

      await CreateFighterScores(match, result);
      Context.MatchResults.Add(new Models.Matches.MatchResult()
      {
        MatchId = match.Id,
        ResultJson = JsonConvert.SerializeObject(result.AsDto(), new JsonSerializerSettings()
        {
          ContractResolver = new CamelCasePropertyNamesContractResolver(),
        }).Compress(),
      });
    }

    private async Task CreateFighterScores(Match match, IMatchResult result)
    {
      foreach (var fighterScore in result.Scores)
      {
        if (!await Context.Fighters.AnyAsync(o => o.Id == fighterScore.Id))
        {
          continue;
        }

        Context.MatchScoreFighters.Add(new MatchScoreFighter()
        {
          Created = dateTimeProvider.Now,
          FighterId = fighterScore.Id,
          MatchId = match.Id,
          RoundsAlive = fighterScore.RoundsAlive,
          Powerlevel = fighterScore.Powerlevel,
          TotalDamageDone = fighterScore.TotalDamageDone,
          MaxEnergy = fighterScore.MaxEnergy,
          MaxHealth = fighterScore.MaxHealth,
          TotalDamageTaken = fighterScore.TotalDamageTaken,
          TotalDeaths = fighterScore.TotalDeaths,
          TotalDistanceTraveled = fighterScore.TotalDistanceTraveled,
          TotalEnergyUsed = fighterScore.TotalEnergyUsed,
          TotalKills = fighterScore.TotalKills,
          TotalRegeneratedEnergy = fighterScore.TotalRegeneratedEnergy,
          TotalRegeneratedHealth = fighterScore.TotalRegeneratedHealth,
        });
      }
    }
  }
}
