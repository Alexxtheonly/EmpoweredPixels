using System;

namespace EmpoweredPixels.Utilities.EloCalculation
{
  public interface IEloRating
  {
    Guid FighterId { get; set; }

    int CurrentElo { get; set; }

    int PreviousElo { get; set; }
  }
}
