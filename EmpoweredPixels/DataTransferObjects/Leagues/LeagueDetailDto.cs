using System.Collections.Generic;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueDetailDto : LeagueDto
  {
    public IEnumerable<LeagueSubscriptionDto> Subscriptions { get; set; }
  }
}
