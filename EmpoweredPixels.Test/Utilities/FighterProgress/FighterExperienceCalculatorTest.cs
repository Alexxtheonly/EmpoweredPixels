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
      fighterExperienceCalculator = new FighterExperienceCalculator(new ContributionPointCalculator(), datetimeprovider.Object);
    }

    [Theory]
    [InlineData(12500, 5, 1415, 5873)]
    [InlineData(325000, 22, 25979, 29968)]
    [InlineData(550000, 29, 9445, 40610)]
    public void ShouldReturnFighterLevel(long experience, int expectedLevel, int expectedExperience, int expectedRequiredExperience)
    {
      var fighterlevel = fighterExperienceCalculator.GetLevel(new FighterExperience() { Points = experience });
      Assert.Equal(expectedLevel, fighterlevel.Level);
      Assert.Equal(expectedExperience, fighterlevel.Experience);
      Assert.Equal(expectedRequiredExperience, fighterlevel.RequiredExperience);
    }

    [Theory]
    [InlineData(1, 1000)]
    [InlineData(8, 9849)]
    [InlineData(16, 21112)]
    [InlineData(24, 32978)]
    [InlineData(32, 45254)]
    [InlineData(5, 5873)]
    [InlineData(14, 18228)]
    [InlineData(30, 42153)]
    public void ShouldReturnCorrectFighterExperience(int level, long expected)
    {
      var actual = fighterExperienceCalculator.GetNeededExperience(level);
      Assert.Equal(expected, actual);
    }
  }
}
