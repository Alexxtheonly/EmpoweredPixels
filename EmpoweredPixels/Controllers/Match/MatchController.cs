using System;
using System.Collections.Generic;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Match;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpFightingEngine.Battlefields.Bounds;
using SharpFightingEngine.Battlefields.Plain;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.FighterPositionGenerators;
using SharpFightingEngine.Engines.MoveOrders;
using SharpFightingEngine.Features;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.WinConditions;

namespace EmpoweredPixels.Controllers.Match
{
  public class MatchController : ControllerBase<DatabaseContext, MatchController>
  {
    public MatchController(DatabaseContext context, ILogger<MatchController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet("test")]
    public ActionResult<MatchDto> GetTestMatch()
    {
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

        return cfg;
      }, DeleteMe(20));

      var result = engine.StartMatch();
      var dto = result.AsDto();
      return Ok(dto);
    }

    private IEnumerable<GenericFighter> DeleteMe(int fighterCount)
    {
      Random random = new Random();

      const int min = 5;
      const int max = 25;

      for (int i = 0; i < fighterCount; i++)
      {
        yield return new GenericFighter()
        {
          Id = Guid.NewGuid(),
          Accuracy = random.Next(min, max),
          Agility = random.Next(min, max),
          Expertise = random.Next(min, max),
          Power = random.Next(min, max),
          Regeneration = random.Next(min, max),
          Speed = random.Next(min, max),
          Stamina = random.Next(min, max),
          Toughness = random.Next(min, max),
          Vision = random.Next(min, max),
          Vitality = random.Next(min, max),
        };
      }
    }
  }
}
