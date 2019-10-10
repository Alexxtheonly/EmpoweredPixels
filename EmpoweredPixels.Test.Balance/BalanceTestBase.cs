using System;
using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Factories.Matches;
using SharpFightingEngine.Battlefields.Constants;
using SharpFightingEngine.Constants;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Test.Balance
{
  public class BalanceTestBase
  {
    protected static TestFighterFactory TestFighterFactory => new TestFighterFactory();

    protected EngineFactory EngineFactory => new EngineFactory();

    protected MatchOptionsDto MatchOptions => new MatchOptionsDto()
    {
      ActionsPerRound = 2,
      Battlefield = BattlefieldConstants.Plain,
      Bounds = BoundsConstants.Tiny,
      Features = new Guid[]
      {
        FeatureConstants.ApplyBuff,
        FeatureConstants.ApplyCondition,
      },
      MoveOrder = MoveOrderConstants.AllRandom,
      PositionGenerator = FighterPositionGeneratorConstants.AllRandom,
      StaleCondition = StaleConditionConstants.NoWinnerCanBeDetermined,
      WinCondition = WinConditionConstants.LastManStanding,
    };

    protected IEnumerable<TestResult> CalculateBalance(IFighterStats fighterA, IFighterStats fighterB, int fightsCount)
    {
      var resultFighterA = new TestResult()
      {
        Fighter = fighterA,
        TotalMatches = fightsCount,
      };

      var resultFighterB = new TestResult()
      {
        Fighter = fighterB,
        TotalMatches = fightsCount,
      };

      for (int i = 0; i < fightsCount; i++)
      {
        var result = GetEngine(fighterA, fighterB).StartMatch();

        var contributionA = result.Contributions.First(o => o.FighterId == fighterA.Id);
        var contributionB = result.Contributions.First(o => o.FighterId == fighterB.Id);

        if (!contributionA.HasWon && !contributionB.HasWon)
        {
          resultFighterA.Draws++;
          resultFighterB.Draws++;
          continue;
        }

        if (contributionA.HasWon)
        {
          resultFighterA.Wins++;
          resultFighterB.Loses++;
        }

        if (contributionB.HasWon)
        {
          resultFighterA.Loses++;
          resultFighterB.Wins++;
        }
      }

      return new TestResult[]
      {
        resultFighterA,
        resultFighterB
      };
    }

    private SharpFightingEngine.Engines.Engine GetEngine(IFighterStats fighterA, IFighterStats fighterB)
    {
      fighterA.DamageTaken = 0;
      fighterB.DamageTaken = 0;

      fighterA.States.Clear();
      fighterB.States.Clear();

      return EngineFactory.GetEngine(
          new IFighterStats[]
        {
          fighterA,
          fighterB,
        }, MatchOptions);
    }

    protected class TestResult
    {
      public IFighterStats Fighter { get; set; }

      public int Wins { get; set; }

      public int Draws { get; set; }

      public int Loses { get; set; }

      public int TotalMatches { get; set; }

      public double WinRate => Wins / (double)TotalMatches;

      public double DrawRate => Draws / (double)TotalMatches;

      public double LoseRate => Loses / (double)TotalMatches;
    }
  }
}
