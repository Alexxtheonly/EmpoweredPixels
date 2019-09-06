namespace EmpoweredPixels.DataTransferObjects.Roster
{
  public class FighterStatForecastDto
  {
    public int Health { get; set; }

    public int HealthRegeneration { get; set; }

    public int Energy { get; set; }

    public int EnergyRegeneration { get; set; }

    public float Velocity { get; set; }

    public float Vision { get; set; }

    public float CritChance { get; set; }

    public float DodgeChance { get; set; }

    public float MissChance { get; set; }

    public float PotentialPower { get; set; }

    public float PotentialDefense { get; set; }
  }
}
