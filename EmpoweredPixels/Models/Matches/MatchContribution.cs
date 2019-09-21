using System;
using EmpoweredPixels.Models.Roster;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Models.Matches
{
  public class MatchContribution : FighterContribution
  {
    public Guid MatchId { get; set; }

    public virtual Fighter Fighter { get; set; }

    public virtual Match Match { get; set; }
  }
}
