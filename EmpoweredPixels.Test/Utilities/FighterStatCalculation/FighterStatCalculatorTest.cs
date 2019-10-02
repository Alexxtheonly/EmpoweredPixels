using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using SharpFightingEngine.Fighters;
using System.Linq;
using Xunit;

namespace EmpoweredPixels.Test.Utilities.FighterStatCalculation
{
  public class FighterStatCalculatorTest
  {
    private readonly FighterStatCalculator fighterStatCalculator = new FighterStatCalculator();

    [Fact]
    public void ShouldCalculateCorrectStats()
    {
      var fighter = new Fighter()
      {
        Accuracy = 80,
        Agility = 80,
        Armor = 80,
        ConditionPower = 80,
        Ferocity = 50,
        Precision = 20,
        HealingPower = 80,
        Power = 80,
        Speed = 10,
        Vision = 10,
        Vitality = 80,
      };

      var equipment = new Equipment[]
      {
        new Equipment()
        {
          Armor = 80,
          SocketStones = Enumerable.Empty<SocketStone>(),
        },
        new Equipment()
        {
          Armor = 110,
          SocketStones = Enumerable.Empty<SocketStone>(),
        },
        new Equipment()
        {
          Armor = 90,
          SocketStones = Enumerable.Empty<SocketStone>(),
        },
        new Equipment()
        {
          Power = 150,
          SocketStones = new SocketStone[]
          {
            new SocketStone()
            {
              Stat = SocketStat.Ferocity,
              Rarity = ItemRarity.Fabled,
              Enhancement = 2,
            },
          }
        },
      };

      fighter.Equipment = equipment;

      var actual = fighterStatCalculator.Calculate(fighter);

      var expected = default(Stats);
      expected.Accuracy = 80;
      expected.Agility = 80;
      expected.Armor = 360;
      expected.ConditionPower = 80;
      expected.Ferocity = 55;
      expected.HealingPower = 80;
      expected.Power = 230;
      expected.Precision = 20;
      expected.Speed = 10;
      expected.Vision = 10;
      expected.Vitality = 80;

      Assert.Equal(expected, actual);
    }
  }
}
