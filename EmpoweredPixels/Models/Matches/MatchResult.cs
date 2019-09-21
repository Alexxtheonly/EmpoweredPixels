using System;

namespace EmpoweredPixels.Models.Matches
{
  public class MatchResult
  {
    public Guid Id { get; set; }

    public Guid MatchId { get; set; }

    public byte[] RoundTicks { get; set; }
  }
}
