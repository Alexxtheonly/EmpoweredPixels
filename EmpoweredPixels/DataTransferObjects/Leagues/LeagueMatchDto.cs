using System;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueMatchDto
  {
    public int LeagueId { get; set; }

    public Guid MatchId { get; set; }

    public DateTimeOffset Started { get; set; }

    public bool HasWinner { get; set; }

    public string WinnerFighterName { get; set; }

    public string WinnerUser { get; set; }
  }
}
