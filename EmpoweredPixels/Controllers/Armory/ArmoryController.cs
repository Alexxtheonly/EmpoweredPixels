using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Armory;
using EmpoweredPixels.Exceptions.Roster;
using EmpoweredPixels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Armory
{
  public class ArmoryController : ControllerBase<DatabaseContext, ArmoryController>
  {
    public ArmoryController(DatabaseContext context, ILogger<ArmoryController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FighterArmoryDto>> GetFighterArmory(Guid id)
    {
      var fighter = await Context.Fighters
        .Include(o => o.User)
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

      var armory = new FighterArmoryDto()
      {
        FighterId = fighter.Id,
        FighterName = fighter.Name,
        Level = 1,
        UserId = fighter.UserId,
        Username = fighter.User.Name,
        EloRating = eloRating?.CurrentElo,
        EloRatingChange = eloRating?.CurrentElo - eloRating?.PreviousElo,
        LastEloRatingUpdate = eloRating?.LastUpdate,
        Kills = kills,
        Deaths = deaths,
        KillDeathRatio = kills / (double)deaths,
      };

      return Ok(armory);
    }
  }
}
