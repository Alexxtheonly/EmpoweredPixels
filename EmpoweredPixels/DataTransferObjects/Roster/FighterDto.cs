using System;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.DataTransferObjects.Roster
{
  public class FighterDto : IStats
  {
    public Guid Id { get; set; }

    public string Name { get; set; }

    public long UserId { get; set; }

    public float Accuracy { get; set; }

    public float Power { get; set; }

    public float Expertise { get; set; }

    public float Agility { get; set; }

    public float Toughness { get; set; }

    public float Vitality { get; set; }

    public float Speed { get; set; }

    public float Stamina { get; set; }

    public float Regeneration { get; set; }

    public float Vision { get; set; }

    public IStats Clone()
    {
      return this;
    }
  }
}
