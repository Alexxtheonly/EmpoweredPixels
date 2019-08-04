using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class RoundScoreDto : TickDto
  {
    public Guid Id { get; set; }

    public int Powerlevel { get; set; }

    public int DamageDone { get; set; }

    public int DamageTaken { get; set; }

    public int Deaths { get; set; }

    public float DistanceTraveled { get; set; }

    public int Energy { get; set; }

    public int EnergyUsed { get; set; }

    public int Health { get; set; }

    public int Kills { get; set; }

    public int RestoredEnergy { get; set; }

    public int RestoredHealth { get; set; }

    public int Round { get; set; }
  }
}
