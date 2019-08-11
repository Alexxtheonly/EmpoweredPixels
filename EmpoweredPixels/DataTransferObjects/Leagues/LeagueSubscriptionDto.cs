using System;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueSubscriptionDto
  {
    public int LeagueId { get; set; }

    public Guid FighterId { get; set; }
  }
}
