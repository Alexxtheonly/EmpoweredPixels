using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Utilities.EnhancementCalculation;
using Moq;
using Xunit;

namespace EmpoweredPixels.Test.Utilities.EnhancementCalculation
{
  public class EnhancementProbabilityTest
  {
    private readonly IEnhancementProbability probability = new EnhancementProbability();

    [Theory]
    [InlineData(1, 0.9)]
    [InlineData(2, 0.8)]
    [InlineData(3, 0.7)]
    [InlineData(4, 0.6)]
    [InlineData(5, 0.5)]
    [InlineData(6, 0.5)]
    [InlineData(7, 0.5)]
    [InlineData(8, 0.5)]
    [InlineData(9, 0.5)]
    [InlineData(10, 0.5)]
    [InlineData(20, 0.5)]
    [InlineData(100, 0.5)]
    public void ShouldEnhance(int enhancement, float expectedProbability)
    {
      var enhancable = Mock.Of<IEnhancable>();

      enhancable.Enhancement = enhancement;

      const int tries = 10000;
      float successCount = 0;
      for (int i = 0; i < tries; i++)
      {
        successCount += probability.IsSuccess(enhancable) ? 1 : 0;
      }

      var actual = successCount / tries;

      Assert.Equal(expectedProbability, actual, 1);
    }
  }
}
