using System;

namespace EmpoweredPixels.Models.Seasons
{
  public class Season
  {
    public int Id { get; set; }

    public Guid SeasonId { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }
  }
}
