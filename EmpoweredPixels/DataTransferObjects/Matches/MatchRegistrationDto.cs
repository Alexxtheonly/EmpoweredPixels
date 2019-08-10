using System;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class MatchRegistrationDto
  {
    public Guid MatchId { get; set; }

    public Guid FighterId { get; set; }

    public Guid? TeamId { get; set; }

    public string FighterName { get; set; }

    public string Username { get; set; }

    public DateTimeOffset Joined { get; set; }
  }
}
