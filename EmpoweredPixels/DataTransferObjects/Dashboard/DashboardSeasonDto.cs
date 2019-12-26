using System;

namespace EmpoweredPixels.DataTransferObjects.Dashboard
{
  public class DashboardSeasonDto
  {
    public Guid SeasonId { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public int Position { get; set; }
  }
}
