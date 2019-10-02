using System;
using System.Collections.Generic;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Rewards;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Models.Items
{
  public class Equipment : IStats, IEnhancable, IReward
  {
    public Guid Id { get; set; }

    public Guid Type { get; set; }

    public long UserId { get; set; }

    public Guid? FighterId { get; set; }

    public int Power { get; set; }

    public int ConditionPower { get; set; }

    public int Precision { get; set; }

    public int Ferocity { get; set; }

    public int Accuracy { get; set; }

    public int Agility { get; set; }

    public int Armor { get; set; }

    public int Vitality { get; set; }

    public int HealingPower { get; set; }

    public int Speed { get; set; }

    public int Vision { get; set; }

    public int Level { get; set; }

    public ItemRarity Rarity { get; set; }

    public int Enhancement { get; set; }

    public virtual User User { get; set; }

    public virtual Fighter Fighter { get; set; }

    public IEnumerable<SocketStone> SocketStones { get; set; }

    public IStats Clone()
    {
      throw new NotImplementedException();
    }
  }
}
