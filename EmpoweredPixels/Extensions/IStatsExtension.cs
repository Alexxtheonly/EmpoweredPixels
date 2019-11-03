using System.Collections.Generic;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Models.Items;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Extensions
{
  public static class IStatsExtension
  {
    public static IStats Sum(this IEnumerable<IStats> stats)
    {
      IStats sum = default(Stats);
      foreach (var stat in stats)
      {
        sum = sum.Add(stat);
      }

      return sum;
    }

    public static IStats Add(this IStats stats, IStats add)
    {
      var added = default(Stats);

      added.Accuracy = stats.Accuracy + add.Accuracy;
      added.Agility = stats.Agility + add.Agility;
      added.Armor = stats.Armor + add.Armor;
      added.ConditionPower = stats.ConditionPower + add.ConditionPower;
      added.Ferocity = stats.Ferocity + add.Ferocity;
      added.HealingPower = stats.HealingPower + add.HealingPower;
      added.Power = stats.Power + add.Power;
      added.Precision = stats.Precision + add.Precision;
      added.Speed = stats.Speed + add.Speed;
      added.Vision = stats.Vision + add.Vision;
      added.Vitality = stats.Vitality + add.Vitality;
      added.ParryChance = stats.ParryChance + add.ParryChance;

      return added;
    }

    public static IStats Add(this IStats stats, SocketStone socketStone)
    {
      var added = default(Stats).Add(stats);

      switch (socketStone.Stat)
      {
        case SocketStat.Accuracy:
          added.Accuracy += socketStone.GetValue();
          break;
        case SocketStat.Power:
          added.Power += socketStone.GetValue();
          break;
        case SocketStat.ConditionPower:
          added.ConditionPower += socketStone.GetValue();
          break;
        case SocketStat.Precision:
          added.Precision += socketStone.GetValue();
          break;
        case SocketStat.Ferocity:
          added.Ferocity += socketStone.GetValue();
          break;
        case SocketStat.Armor:
          added.Armor += socketStone.GetValue();
          break;
        case SocketStat.Vitality:
          added.Vitality += socketStone.GetValue();
          break;
        case SocketStat.HealingPower:
          added.HealingPower += socketStone.GetValue();
          break;
        case SocketStat.Speed:
          added.Speed += socketStone.GetValue();
          break;
        case SocketStat.Vision:
          added.Vision += socketStone.GetValue();
          break;
        case SocketStat.None:
        default:
          break;
      }

      return added;
    }
  }
}
