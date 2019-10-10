using System.Linq;
using EmpoweredPixels.Enums.Equipment;
using Xunit;

namespace EmpoweredPixels.Test.Balance.Weapon
{
  public class WeaponBalanceTest : BalanceTestBase
  {
    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void BowShouldHaveWinrateAgainstGreatsword(int level, int fightCount, double expectedWinrate)
    {
      var bow = TestFighterFactory.GetBowFighter(level, ItemRarity.Mythic);
      var greatsword = TestFighterFactory.GetGreatswordFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(bow, greatsword, fightCount);

      var bowResults = balanceResults.First(o => o.Fighter.Id == bow.Id);
      Assert.Equal(bowResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void BowShouldHaveWinrateAgainstBow(int level, int fightCount, double expectedWinrate)
    {
      var bow = TestFighterFactory.GetBowFighter(level, ItemRarity.Mythic);
      var other = TestFighterFactory.GetBowFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(bow, other, fightCount);

      var bowResults = balanceResults.First(o => o.Fighter.Id == bow.Id);
      Assert.Equal(bowResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void GreatswordShouldHaveWinrateAgainstGreatsword(int level, int fightCount, double expectedWinrate)
    {
      var greatsword = TestFighterFactory.GetGreatswordFighter(level, ItemRarity.Mythic);
      var other = TestFighterFactory.GetGreatswordFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(greatsword, other, fightCount);

      var greatswordResults = balanceResults.First(o => o.Fighter.Id == greatsword.Id);
      Assert.Equal(greatswordResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void DaggerShouldHaveWinrateAgainstGreatsword(int level, int fightCount, double expectedWinrate)
    {
      var dagger = TestFighterFactory.GetDaggerFighter(level, ItemRarity.Mythic);
      var greatsword = TestFighterFactory.GetGreatswordFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(dagger, greatsword, fightCount);

      var daggerResults = balanceResults.First(o => o.Fighter.Id == dagger.Id);
      Assert.Equal(daggerResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void DaggerShouldHaveWinrateAgainstBow(int level, int fightCount, double expectedWinrate)
    {
      var dagger = TestFighterFactory.GetDaggerFighter(level, ItemRarity.Mythic);
      var bow = TestFighterFactory.GetBowFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(dagger, bow, fightCount);

      var daggerResults = balanceResults.First(o => o.Fighter.Id == dagger.Id);
      Assert.Equal(daggerResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void GlaiveShouldHaveWinrateAgainstBow(int level, int fightCount, double expectedWinrate)
    {
      var glaive = TestFighterFactory.GetGaiveFighter(level, ItemRarity.Mythic);
      var bow = TestFighterFactory.GetBowFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(glaive, bow, fightCount);

      var glaiveResults = balanceResults.First(o => o.Fighter.Id == glaive.Id);
      Assert.Equal(glaiveResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void GlaiveShouldHaveWinrateAgainstDagger(int level, int fightCount, double expectedWinrate)
    {
      var glaive = TestFighterFactory.GetGaiveFighter(level, ItemRarity.Mythic);
      var other = TestFighterFactory.GetDaggerFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(glaive, other, fightCount);

      var glaiveResults = balanceResults.First(o => o.Fighter.Id == glaive.Id);
      Assert.Equal(glaiveResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void GlaiveShouldHaveWinrateAgainstGreatsword(int level, int fightCount, double expectedWinrate)
    {
      var glaive = TestFighterFactory.GetGaiveFighter(level, ItemRarity.Mythic);
      var other = TestFighterFactory.GetGreatswordFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(glaive, other, fightCount);

      var glaiveResults = balanceResults.First(o => o.Fighter.Id == glaive.Id);
      Assert.Equal(glaiveResults.WinRate, expectedWinrate, 1);
    }

    [Theory]
    [InlineData(8, 10000, 0.5)]
    [InlineData(16, 10000, 0.5)]
    [InlineData(24, 10000, 0.5)]
    public void GlaiveShouldHaveWinrateAgainstGlaive(int level, int fightCount, double expectedWinrate)
    {
      var glaive = TestFighterFactory.GetGaiveFighter(level, ItemRarity.Mythic);
      var other = TestFighterFactory.GetGaiveFighter(level, ItemRarity.Mythic);

      var balanceResults = CalculateBalance(glaive, other, fightCount);

      var glaiveResults = balanceResults.First(o => o.Fighter.Id == glaive.Id);
      Assert.Equal(glaiveResults.WinRate, expectedWinrate, 1);
    }
  }
}
