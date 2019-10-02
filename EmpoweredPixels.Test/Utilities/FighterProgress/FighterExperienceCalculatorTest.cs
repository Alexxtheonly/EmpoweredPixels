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
    [InlineData(12500, 11, 2875, 3025)]
    [InlineData(325000, 34, 11775, 28900)]
    [InlineData(550000, 40, 36500, 40000)]
    public void ShouldReturnFighterLevel(long experience, int expectedLevel, int expectedExperience, int expectedRequiredExperience)
    {
      var fighterlevel = fighterExperienceCalculator.GetLevel(new FighterExperience() { Points = experience });
      Assert.Equal(expectedLevel, fighterlevel.Level);
      Assert.Equal(expectedExperience, fighterlevel.Experience);
      Assert.Equal(expectedRequiredExperience, fighterlevel.RequiredExperience);
    }

    [Theory]
    [InlineData(1, 25)]
    [InlineData(8, 1600)]
    [InlineData(16, 6400)]
    [InlineData(24, 14400)]
    [InlineData(32, 25600)]
    [InlineData(5, 625)]
    [InlineData(14, 4900)]
    [InlineData(30, 22500)]
    public void ShouldReturnCorrectFighterExperience(int level, long expected)
    {
      var actual = fighterExperienceCalculator.GetNeededExperience(level);
      Assert.Equal(expected, actual);
    }
  }
}
