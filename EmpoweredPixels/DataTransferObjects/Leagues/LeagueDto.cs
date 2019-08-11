using System;

namespace EmpoweredPixels.DataTransferObjects.Leagues
{
  public class LeagueDto
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public int SubscriberCount { get; set; }

    public int MaxPowerlevel { get; set; }

    public bool IsTeam { get; set; }

    public int? TeamSize { get; set; }

    public DateTimeOffset NextMatch { get; set; }
  }
}
