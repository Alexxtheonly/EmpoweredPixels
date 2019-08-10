using System;
using System.Collections.Generic;
using EmpoweredPixels.Interfaces.Identity;

namespace EmpoweredPixels.Models.Matches
{
  public class MatchTeam : IPassword
  {
    public Guid Id { get; set; }

    public Guid MatchId { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public virtual ICollection<MatchRegistration> Registrations { get; set; }
  }
}
