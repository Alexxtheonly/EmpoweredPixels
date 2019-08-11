using EmpoweredPixels.DataTransferObjects.Matches;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueOptionsDto
  {
    public string IntervalCron { get; set; }

    public bool IsTeam { get; set; }

    public int? TeamSize { get; set; }

    public MatchOptionsDto MatchOptions { get; set; }
  }
}
