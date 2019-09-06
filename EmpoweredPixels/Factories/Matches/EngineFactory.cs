using System;
using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.DataTransferObjects.Matches;
using SharpFightingEngine.Battlefields;
using SharpFightingEngine.Battlefields.Bounds;
using SharpFightingEngine.Battlefields.Plain;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Engines.FighterPositionGenerators;
using SharpFightingEngine.Engines.MoveOrders;
using SharpFightingEngine.Features;
using SharpFightingEngine.Fighters;
using SharpFightingEngine.Fighters.Factories;
using SharpFightingEngine.StaleConditions;
using SharpFightingEngine.WinConditions;

namespace EmpoweredPixels.Factories.Matches
{
  public class EngineFactory : IEngineFactory
  {
    private readonly IEnumerable<IBattlefield> battlefields = new IBattlefield[]
     {
      new PlainBattlefield(),
     };

    private readonly IEnumerable<IBounds> bounds = new IBounds[]
    {
      new Tiny(),
      new Small(),
      new Medium(),
      new Big(),
      new Large(),
    };

    private readonly IEnumerable<IWinCondition> winConditions = new IWinCondition[]
    {
      new LastManStandingWinCondition(),
    };

    private readonly IEnumerable<IStaleCondition> staleConditions = new IStaleCondition[]
    {
      new NoWinnerCanBeDeterminedStaleCondition(),
    };

    private readonly IEnumerable<IEngineFeature> engineFeatures = new IEngineFeature[]
    {
      new FeatureRegenerateEnergy(),
      new FeatureRegenerateHealth(),
      new FeatureSacrificeToEntity(),
    };

    private readonly IEnumerable<IFighterPositionGenerator> fighterPositionGenerators = new IFighterPositionGenerator[]
    {
      new AllRandomPositionGenerator(),
    };

    private readonly IEnumerable<IMoveOrder> moveOrders = new IMoveOrder[]
    {
      new AllRandomMoveOrder(),
    };

    public EngineCalculationValues CalculationValues => new EngineCalculationValues();

    public Engine GetEngine(IEnumerable<IFighterStats> fighters, MatchOptionsDto optionsDto)
    {
      var qualified = fighters;
      if (optionsDto.MaxPowerlevel != null)
      {
        qualified = qualified.Where(o => o.PowerLevel() <= optionsDto.MaxPowerlevel);
      }

      if ((optionsDto.BotCount > 0 && optionsDto.BotCount < 1000) && optionsDto.BotPowerlevel > 10)
      {
        qualified = qualified
          .Union(FighterFactory.GetFighters(optionsDto.BotCount.Value, optionsDto.BotPowerlevel.Value));
      }

      return new Engine(
        cfg =>
      {
        cfg.ActionsPerRound = optionsDto.ActionsPerRound;
        cfg.Battlefield = battlefields.FirstOrDefault(o => o.Id == optionsDto.Battlefield);
        cfg.Bounds = bounds.FirstOrDefault(o => o.Id == optionsDto.Bounds);
        cfg.Features = engineFeatures.Where(o => optionsDto.Features.Contains(o.Id)).ToList();
        cfg.MoveOrder = moveOrders.FirstOrDefault(o => o.Id == optionsDto.MoveOrder);
        cfg.PositionGenerator = fighterPositionGenerators.FirstOrDefault(o => o.Id == optionsDto.PositionGenerator);
        cfg.StaleCondition = staleConditions.FirstOrDefault(o => o.Id == optionsDto.StaleCondition);
        cfg.WinCondition = winConditions.FirstOrDefault(o => o.Id == optionsDto.WinCondition);

        return cfg;
      }, qualified.ToList());
    }

    public MatchOptionsDto GetDefaultOptions()
    {
      return new MatchOptionsDto()
      {
        ActionsPerRound = 2,
        Battlefield = battlefields.OfType<PlainBattlefield>().First().Id,
        Bounds = bounds.OfType<Small>().First().Id,
        Features = engineFeatures.Select(o => o.Id),
        MaxFightersPerUser = 1,
        MoveOrder = moveOrders.OfType<AllRandomMoveOrder>().First().Id,
        PositionGenerator = fighterPositionGenerators.OfType<AllRandomPositionGenerator>().First().Id,
        StaleCondition = staleConditions.OfType<NoWinnerCanBeDeterminedStaleCondition>().First().Id,
        WinCondition = winConditions.OfType<LastManStandingWinCondition>().First().Id,
      };
    }

    public IEnumerable<Guid> GetAvailableBounds()
    {
      return bounds.Select(o => o.Id);
    }
  }
}
