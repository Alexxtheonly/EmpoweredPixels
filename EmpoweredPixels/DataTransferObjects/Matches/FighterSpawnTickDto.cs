using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class FighterSpawnTickDto : TickDto
  {
    public Guid FighterId { get; set; }

    public bool Spawned => true;

    public int Health { get; set; }

    public int Energy { get; set; }

    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public float PositionZ { get; set; }
  }
}
