using System;
using System.Linq;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Models.Roster;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Utilities.FighterStatCalculation
{
  public class FighterStatCalculator : IFighterStatCalculator
  {
    public IStats Calculate(Fighter fighter)
    {
      if (fighter.Equipment is null)
      {
        throw new ArgumentNullException(nameof(fighter.Equipment));
      }

      if (fighter.Equipment.Any(o => o.SocketStones == null))
      {
        throw new ArgumentNullException(nameof(Equipment.SocketStones));
      }

      var equipmentStats = fighter.Equipment.Sum();
      var equipmentAndFighterStats = equipmentStats.Add(fighter);
      var equipmentFighterAndStonesStats = equipmentAndFighterStats;
      foreach (var socketStone in fighter.Equipment.SelectMany(o => o.SocketStones))
      {
        equipmentFighterAndStonesStats = equipmentFighterAndStonesStats.Add(socketStone);
      }

      equipmentFighterAndStonesStats.Level = fighter.Level;

      return equipmentFighterAndStonesStats;
    }
  }
}
