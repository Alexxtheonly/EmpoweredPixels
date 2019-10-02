using System;
using System.Collections.Generic;
using EmpoweredPixels.DataTransferObjects.Items;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.DataTransferObjects.Roster
{
  public class FighterDto : IStats
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public long UserId { get; set; }

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

    public IEnumerable<EquipmentDto> Equipment { get; set; }

    public IStats Clone()
    {
      return this;
    }
  }
}
