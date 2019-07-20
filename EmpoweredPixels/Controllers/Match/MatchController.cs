using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Hubs.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharpFightingEngine.Battlefields.Bounds;
using SharpFightingEngine.Battlefields.Plain;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.FighterPositionGenerators;
using SharpFightingEngine.Engines.MoveOrders;
using SharpFightingEngine.Features;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.StaleConditions;
using SharpFightingEngine.WinConditions;

namespace EmpoweredPixels.Controllers.Matches
{
  public class MatchController : ControllerBase<DatabaseContext, MatchController>
  {
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly IHubContext<MatchHub, IMatchClient> matchHubContext;

    public MatchController(
      DatabaseContext context,
      ILogger<MatchController> logger,
      IMapper mapper,
      IDateTimeProvider dateTimeProvider,
      IHubContext<MatchHub, IMatchClient> matchHubContext)
      : base(context, logger, mapper)
    {
      this.dateTimeProvider = dateTimeProvider;
      this.matchHubContext = matchHubContext;
    }

    [HttpPut]
    public async Task<ActionResult<MatchDto>> CreateNewMatch()
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      var match = new Match()
      {
        CreatorUserId = userId,
      };

      Context.Add(match);
      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<MatchDto>(match));
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

    [HttpPost("join")]
    public async Task<ActionResult> JoinMatch([FromBody] MatchRegistrationDto dto)
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

      var registration = new MatchRegistration()
      {
        MatchId = match.Id,
        FighterId = fighter.Id,
        Date = dateTimeProvider.Now,
      };
      Context.MatchRegistrations.Add(registration);
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

      StartMatchInternal(match);

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

      return Content(matchResult.ResultJson);
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

    private void StartMatchInternal(Match match)
    {
      match.Started = dateTimeProvider.Now;

      var fighters = match.Registrations
        .Select(o => o.Fighter)
        .Select(o => new GenericFighter()
        {
          Id = o.Id,
          Accuracy = o.Accuracy,
          Agility = o.Agility,
          Expertise = o.Expertise,
          Power = o.Power,
          Regeneration = o.Regeneration,
          Speed = o.Speed,
          Stamina = o.Stamina,
          Toughness = o.Toughness,
          Vision = o.Vision,
          Vitality = o.Vitality,
        });

      var engine = new Engine(
        cfg =>
        {
          cfg.ActionsPerRound = 2;
          cfg.Battlefield = new PlainBattlefield(new Small());
          cfg.Features.Add(new FeatureRegenerateEnergy());
          cfg.Features.Add(new FeatureRegenerateHealth());
          cfg.MoveOrder = new AllRandomMoveOrder();
          cfg.PositionGenerator = new AllRandomPositionGenerator();
          cfg.WinCondition = new LastManStandingWinCondition();
          cfg.StaleCondition = new NoWinnerCanBeDeterminedStaleCondition();

          return cfg;
        }, fighters);

      var result = engine.StartMatch();

      CreateFighterScores(match, result);
      Context.MatchResults.Add(new Models.Matches.MatchResult()
      {
        MatchId = match.Id,
        ResultJson = JsonConvert.SerializeObject(result.AsDto()),
      });
    }

    private void CreateFighterScores(Match match, IMatchResult result)
    {
      foreach (var fighterScore in result.Scores)
      {
        Context.MatchScoreFighters.Add(new MatchScoreFighter()
        {
          Created = dateTimeProvider.Now,
          FighterId = fighterScore.Id,
          MatchId = match.Id,
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
