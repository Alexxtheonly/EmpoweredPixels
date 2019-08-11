using System;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueSubscriptionDto
  {
    public int LeagueId { get; set; }

    public Guid FighterId { get; set; }

    public string FighterName { get; set; }

    public string User { get; set; }
  }
}
