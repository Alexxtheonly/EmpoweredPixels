using System;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueHighscoreDto
  {
    public Guid FighterId { get; set; }

    public string FighterName { get; set; }

    public string Username { get; set; }

    public int Score { get; set; }
  }
}
