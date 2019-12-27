using EmpoweredPixels.Models.Ratings;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Utilities.LeageExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EmpoweredPixels.Test.Utilities.LeageExecution
{
  public class LeagueDivisionDividerTest
  {
    private static readonly Random random = new Random();
    private readonly ILeagueDivisionDivider divisionDivider = new EqualPartLeagueDivisionDivider();

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    [InlineData(13)]
    [InlineData(14)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(18)]
    [InlineData(19)]
    [InlineData(141)]
    public void ShouldDivideIntoEqualParts(int fighterCount)
    {
      var divisions = divisionDivider.GetDivisions(GetFighters(fighterCount));

      Assert.False(divisions.Skip(1).Where(o => o.Count() < 4).Any());
      Assert.Equal(fighterCount, divisions.SelectMany(o => o.AsEnumerable()).Count());
    }

    private static IEnumerable<Fighter> GetFighters(int count)
    {
      for (int i = 0; i < count; i++)
      {
        yield return new Fighter()
        {
          EloRating = new FighterEloRating()
          {
            CurrentElo = random.Next(1000, 1900),
          }
        };
      }
    }
  }
}
