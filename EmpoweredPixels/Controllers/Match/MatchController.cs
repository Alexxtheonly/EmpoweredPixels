﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Exceptions.Leagues;
using EmpoweredPixels.Exceptions.Matches;
using EmpoweredPixels.Exceptions.Roster;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.Paging;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Controllers.Matches
{
  public class MatchController : ControllerBase<DatabaseContext, MatchController>
  {
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly IEngineFactory engineFactory;

    public MatchController(
      DatabaseContext context,
      ILogger<MatchController> logger,
      IMapper mapper,
      IDateTimeProvider dateTimeProvider,
      IEngineFactory engineFactory)
      : base(context, logger, mapper)
    {
      this.dateTimeProvider = dateTimeProvider;
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
        return BadRequest(new InvalidMatchException());
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

      if (match == null)
      {
        return BadRequest(new InvalidMatchException());
      }

      if (fighter == null)
      {
        return BadRequest(new InvalidFighterException());
      }

      if (match.Options.MaxFightersPerUser != null &&
        match.Registrations
        .Where(o => o.Fighter.UserId == userId)
        .Count() >= match.Options.MaxFightersPerUser)
      {
        return BadRequest(new MatchFighterLimitExceededException());
      }

      if (match.Options.MaxPowerlevel != null &&
        fighter.PowerLevel() > match.Options.MaxPowerlevel)
      {
        return BadRequest(new IllegalFighterPowerlevelException());
      }

      var registration = new MatchRegistration()
      {
        MatchId = match.Id,
        FighterId = fighter.Id,
        Date = dateTimeProvider.Now,
      };
      Context.MatchRegistrations.Add(registration);
      await Context.SaveChangesAsync();

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
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .FirstOrDefaultAsync(o => o.Id == dto.MatchId);

      if (match == null)
      {
        return BadRequest(new InvalidMatchException());
      }

      if (match.Options.MaxFightersPerUser != null &&
        match.Registrations
        .Where(o => o.Fighter.UserId == userId && o.FighterId != dto.FighterId)
        .Count() >= match.Options.MaxFightersPerUser)
      {
        return BadRequest(new MatchFighterLimitExceededException());
      }

      var team = await Context.MatchTeams
        .FirstOrDefaultAsync(o => o.Id == dto.Id);

      if (team == null)
      {
        return BadRequest(new InvalidTeamException());
      }

      if (!team.IsValidPassword(dto.Password))
      {
        return BadRequest(new InvalidTeamPasswordException());
      }

      var fighter = await Context.Fighters
        .Where(o => o.UserId == userId)
        .FirstOrDefaultAsync(o => o.Id == dto.FighterId);

      if (fighter == null)
      {
        return BadRequest(new InvalidFighterException());
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

      if (match == null)
      {
        return BadRequest(new InvalidMatchException());
      }

      if (fighter == null)
      {
        return BadRequest(new InvalidFighterException());
      }

      var registration = await Context.MatchRegistrations
        .FirstOrDefaultAsync(o => o.FighterId == dto.FighterId && o.MatchId == dto.MatchId);
      if (registration == null)
      {
        return BadRequest(new InvalidMatchRegistrationException());
      }

      Context.MatchRegistrations.Remove(registration);
      await Context.SaveChangesAsync();

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
        return BadRequest(new InvalidMatchException());
      }

      var registration = await Context.MatchRegistrations
        .AsTracking()
        .Include(o => o.Fighter)
        .Where(o => o.Fighter.UserId == userId)
        .Where(o => o.MatchId == match.Id)
        .FirstOrDefaultAsync(o => o.FighterId == dto.FighterId);

      if (registration == null)
      {
        return BadRequest(new InvalidMatchRegistrationException());
      }

      if (registration.TeamId == dto.Id)
      {
        return Ok();
      }

      registration.TeamId = null;
      await Context.SaveChangesAsync();

      return Ok();
    }

    [HttpGet("{id}/roundticks")]
    public async Task<ActionResult<IEnumerable<RoundTickDto>>> GetMatchRoundTicks(Guid id)
    {
      var matchResult = await Context.MatchResults
        .FirstOrDefaultAsync(o => o.MatchId == id);

      if (matchResult == null)
      {
        return BadRequest(new InvalidMatchResultException());
      }

      return Content(matchResult.RoundTicks.Decompress(), "application/json");
    }

    [HttpGet("{id}/fighterscores")]
    public async Task<ActionResult<IEnumerable<MatchScoreFighterDto>>> GetMatchFighterScores(Guid id, [FromServices] IContributionPointCalculator pointCalculator)
    {
      var scores = await Context.MatchScoreFighters
        .Where(o => o.MatchId == id)
        .ProjectTo<MatchScoreFighterDto>(Mapper.ConfigurationProvider)
        .ToListAsync();

      foreach (var score in scores)
      {
        var contribution = await Context.MatchContributions
          .FirstOrDefaultAsync(o => o.MatchId == id && o.FighterId == score.FighterId);
        if (contribution == null)
        {
          throw new NullReferenceException($"{nameof(contribution)} is null. MatchId {id}, FighterId {score.FighterId}");
        }

        score.Points = pointCalculator.Calculate(contribution);
      }

      return Ok(scores.OrderByDescending(o => o.Points));
    }
  }
}
