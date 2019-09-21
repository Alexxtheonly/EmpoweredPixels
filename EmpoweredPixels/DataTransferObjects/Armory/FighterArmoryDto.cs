using System;

namespace EmpoweredPixels.DataTransferObjects.Armory
{
  public class FighterArmoryDto
  {
    public Guid FighterId { get; set; }

    public string FighterName { get; set; }

    public long UserId { get; set; }

    public string Username { get; set; }

    public int? EloRating { get; set; }

    public int? EloRatingChange { get; set; }

    public DateTimeOffset? LastEloRatingUpdate { get; set; }

    public int Level { get; set; }

    public double KillDeathRatio { get; set; }

    public int Kills { get; set; }

    public int Deaths { get; set; }
  }
}
