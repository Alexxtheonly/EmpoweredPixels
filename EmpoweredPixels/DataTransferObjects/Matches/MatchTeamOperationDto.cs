using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class MatchTeamOperationDto
  {
    public Guid Id { get; set; }

    public Guid MatchId { get; set; }

    public Guid? FighterId { get; set; }

    public string Password { get; set; }
  }
}
