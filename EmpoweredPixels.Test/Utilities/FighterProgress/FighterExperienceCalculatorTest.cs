﻿using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.FighterProgress;
using Moq;
using Xunit;

namespace EmpoweredPixels.Test.Utilities.FighterProgress
{
  public class FighterExperienceCalculatorTest
  {
    private readonly Mock<IDateTimeProvider> datetimeprovider = new Mock<IDateTimeProvider>();

    private readonly FighterExperienceCalculator fighterExperienceCalculator;

    public FighterExperienceCalculatorTest()
    {
      fighterExperienceCalculator = new FighterExperienceCalculator(datetimeprovider.Object, TestUtilities.GetTestContext<DatabaseContext>());
    }

    [Theory]
    [InlineData(12500, 7, 500, 2000)]
    [InlineData(325000, 61, 2000, 9000)]
    [InlineData(550000, 82, 8000, 12000)]
    public void ShouldReturnFighterLevel(long experience, int expectedLevel, int expectedExperience, int expectedRequiredExperience)
    {
      var fighterlevel = fighterExperienceCalculator.GetLevel(new FighterExperience() { Points = experience });
      Assert.Equal(expectedLevel, fighterlevel.Level);
      Assert.Equal(expectedExperience, fighterlevel.Experience);
      Assert.Equal(expectedRequiredExperience, fighterlevel.RequiredExperience);
    }

    [Theory]
    [InlineData(1, 2000)]
    [InlineData(8, 3000)]
    [InlineData(16, 4000)]
    [InlineData(24, 5000)]
    [InlineData(32, 6000)]
    [InlineData(5, 2000)]
    [InlineData(14, 3000)]
    [InlineData(30, 5000)]
    public void ShouldReturnCorrectFighterExperience(int level, long expected)
    {
      var actual = fighterExperienceCalculator.GetNeededExperience(level);
      Assert.Equal(expected, actual);
    }
  }
}
