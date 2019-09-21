using System;

namespace EmpoweredPixels.Models.Matches
{
  public abstract class MatchScoreBase
  {
    public Guid MatchId { get; set; }

    public int RoundsAlive { get; set; }

    public int TotalDamageDone { get; set; }

    public int TotalDamageTaken { get; set; }

    public int TotalEnergyUsed { get; set; }

    public int TotalKills { get; set; }

    public int TotalDeaths { get; set; }

    public float TotalDistanceTraveled { get; set; }

    public int TotalRegeneratedHealth { get; set; }

    public int TotalRegeneratedEnergy { get; set; }

    public DateTimeOffset Created { get; set; }
  }
}
