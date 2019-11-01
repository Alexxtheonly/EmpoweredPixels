using System;

namespace EmpoweredPixels.DataTransferObjects.Dashboard
{
  public class DashboardFighterResultDto
  {
    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public Guid FighterId { get; set; }

    public string FighterName { get; set; }

    public Guid MatchId { get; set; }

    public DateTimeOffset? MatchStart { get; set; }

    public bool IsFirst { get; set; }

    public bool IsSecond { get; set; }

    public bool IsThird { get; set; }
  }
}
