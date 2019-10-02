using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Roster;
using EmpoweredPixels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Players
{
  public class PlayerController : ControllerBase<DatabaseContext, PlayerController>
  {
    public PlayerController(DatabaseContext context, ILogger<PlayerController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet("experience")]
    public ActionResult<FighterExperienceDto> GetPlayerexperience()
    {
      return Ok(new FighterExperienceDto()
      {
        Level = 1,
        CurrentExp = 0,
        LevelExp = 5000,
      });
    }
  }
}
