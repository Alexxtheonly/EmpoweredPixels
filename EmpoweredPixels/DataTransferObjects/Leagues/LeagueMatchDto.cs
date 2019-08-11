using System;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueMatchDto
  {
    public int LeagueId { get; set; }

    public Guid MatchId { get; set; }

    public DateTimeOffset Started { get; set; }
  }
}
