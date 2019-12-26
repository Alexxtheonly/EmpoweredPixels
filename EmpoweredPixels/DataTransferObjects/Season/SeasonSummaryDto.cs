using System;

namespace EmpoweredPixels.DataTransferObjects.Season
{
  public class SeasonSummaryDto
  {
    public Guid SeasonId { get; set; }

    public int SalvageValue { get; set; }

    public int Bonus { get; set; }

    public int? Position { get; set; }
  }
}
