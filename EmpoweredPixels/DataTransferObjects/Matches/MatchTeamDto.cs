using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class MatchTeamDto
  {
    public Guid Id { get; set; }

    public Guid MatchId { get; set; }

    public bool HasPassword { get; set; }
  }
}
