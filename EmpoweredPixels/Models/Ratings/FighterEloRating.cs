using System;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Utilities.EloCalculation;

namespace EmpoweredPixels.Models.Ratings
{
  public class FighterEloRating : IEloRating
  {
    public long Id { get; set; }

    public Guid FighterId { get; set; }

    public int CurrentElo { get; set; }

    public int PreviousElo { get; set; }

    public DateTimeOffset? LastUpdate { get; set; }

    public virtual Fighter Fighter { get; set; }
  }
}
