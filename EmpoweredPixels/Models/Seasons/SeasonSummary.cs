using System;
using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Models.Seasons
{
  public class SeasonSummary
  {
    public Guid Id { get; set; }

    public int SeasonId { get; set; }

    public long UserId { get; set; }

    public int SalvageValue { get; set; }

    public int Bonus { get; set; }

    public int? Position { get; set; }

    public User User { get; set; }

    public Season Season { get; set; }
  }
}
