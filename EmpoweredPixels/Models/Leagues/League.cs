using System.Collections.Generic;
using EmpoweredPixels.DataTransferObjects.Leagues;

namespace EmpoweredPixels.Models.Leagues
{
  public class League
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public bool IsDeactivated { get; set; }

    public LeagueOptionsDto Options { get; set; }

    public ICollection<LeagueSubscription> Subscriptions { get; set; }
  }
}
