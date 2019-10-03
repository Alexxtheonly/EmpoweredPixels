using System;
using EmpoweredPixels.DataTransferObjects.Roster;

namespace EmpoweredPixels.DataTransferObjects.Armory
{
  public class FighterArmoryDto
  {
    public long UserId { get; set; }

    public string Username { get; set; }

    public float OffensiveRating { get; set; }

    public float DefensiveRating { get; set; }

    public int? EloRating { get; set; }

    public int? EloRatingChange { get; set; }

    public DateTimeOffset? LastEloRatingUpdate { get; set; }

    public double KillDeathRatio { get; set; }

    public int Kills { get; set; }

    public int Deaths { get; set; }

    public FighterDto Fighter { get; set; }
  }
}
