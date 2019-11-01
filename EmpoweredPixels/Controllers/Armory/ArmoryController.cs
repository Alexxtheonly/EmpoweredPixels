using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Armory;
using EmpoweredPixels.DataTransferObjects.Roster;
using EmpoweredPixels.Exceptions.Roster;
using EmpoweredPixels.Models;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using EmpoweredPixels.Utilities.Paging;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Controllers.Armory
{
  public class ArmoryController : ControllerBase<DatabaseContext, ArmoryController>
  {
    public ArmoryController(DatabaseContext context, ILogger<ArmoryController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpPost]
    public async Task<ActionResult<Page<FighterArmoryViewDto>>> GetFighterArmoryPage([FromBody]PagingOptions options)
    {
      return Ok(await Context.Fighters
        .Include(o => o.User)
        .Include(o => o.EloRating)
        .Where(o => o.EloRating != null)
        .OrderByDescending(o => o.EloRating.CurrentElo)
        .Select(o => new FighterArmoryViewDto()
        {
          FighterId = o.Id,
          FighterName = o.Name,
          FighterLevel = o.Level,
          Username = o.User.Name,
          UserId = o.UserId,
          FighterElo = o.EloRating.CurrentElo,
          FighterPreviousElo = o.EloRating.PreviousElo,
        })
        .GetPage(options));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FighterArmoryDto>> GetFighterArmory(Guid id, [FromServices] IFighterStatCalculator fighterStatCalculator)
    {
      var fighter = await Context.Fighters
        .Include(o => o.User)
        .Include(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == id);

      if (fighter == null)
      {
        return BadRequest(new InvalidFighterException());
      }

      var eloRating = await Context.FighterEloRatings.FirstOrDefaultAsync(o => o.FighterId == id);

      var kills = await Context.MatchScoreFighters
        .Where(o => o.FighterId == id)
        .SumAsync(o => o.TotalKills);

      var deaths = await Context.MatchScoreFighters
        .Where(o => o.FighterId == id)
        .SumAsync(o => o.TotalDeaths);

      var calculatedStats = fighterStatCalculator.Calculate(fighter);

      var armory = new FighterArmoryDto()
      {
        UserId = fighter.UserId,
        Username = fighter.User.Name,
        OffensiveRating = calculatedStats.OffensivePowerLevel(),
        DefensiveRating = calculatedStats.DefensivePowerLevel(),
        EloRating = eloRating?.CurrentElo,
        EloRatingChange = eloRating?.CurrentElo - eloRating?.PreviousElo,
        LastEloRatingUpdate = eloRating?.LastUpdate,
        Kills = kills,
        Deaths = deaths,
        KillDeathRatio = deaths == 0 ? kills : kills / (double)deaths,
        Fighter = Mapper.Map<FighterDto>(fighter),
      };

      return Ok(armory);
    }
  }
}
