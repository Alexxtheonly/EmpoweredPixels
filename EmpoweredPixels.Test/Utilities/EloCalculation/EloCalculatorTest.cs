using EmpoweredPixels.Utilities.EloCalculation;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EmpoweredPixels.Test.Utilities.EloCalculation
{
  public class EloCalculatorTest
  {
    private static readonly EloCalculator eloCalculator = new EloCalculator();

    [Fact]
    public void ShouldCalculateCorretEloValues()
    {
      var player0 = Mock.Of<IEloRating>();
      player0.FighterId = new Guid("152D68CE-6D84-4400-87B9-93B40A4576E6");
      player0.PreviousElo = 1500;
      player0.CurrentElo = 1500;

      var player1 = Mock.Of<IEloRating>();
      player1.FighterId = new Guid("04A0BFCE-38BD-4C4D-B31B-52E9EF0567A1");
      player1.PreviousElo = 1500;
      player1.CurrentElo = 1500;

      var player2 = Mock.Of<IEloRating>();
      player2.FighterId = new Guid("26A31E83-1C14-48C1-8D10-E98D85B1E27C");
      player2.PreviousElo = 1500;
      player2.CurrentElo = 1500;

      var ratings = new IEloRating[]
      {
        player0,
        player1,
        player2,
      };

      var positions = new EloPosition[]
      {
        new EloPosition()
        {
          Id = player0.FighterId,
          Points = 200,
        },
        new EloPosition()
        {
          Id = player1.FighterId,
          Points = 300,
        },
        new EloPosition()
        {
          Id = player2.FighterId,
          Points = 100,
        }
      };

      eloCalculator.Calculate(ratings, positions);

      Assert.Equal(1512, player1.CurrentElo);
      Assert.Equal(1500, player0.CurrentElo);
      Assert.Equal(1488, player2.CurrentElo);

      Assert.Equal(1500, player0.PreviousElo);
      Assert.Equal(1500, player1.PreviousElo);
      Assert.Equal(1500, player2.PreviousElo);
    }
  }
}
