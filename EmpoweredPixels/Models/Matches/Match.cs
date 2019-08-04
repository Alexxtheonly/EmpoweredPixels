using System;
using System.Collections.Generic;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Models.Matches
{
  public class Match
  {
    public Guid Id { get; set; }

    public DateTimeOffset? Created { get; set; }

    public DateTimeOffset? Started { get; set; }

    public long? CreatorUserId { get; set; }

    public MatchOptionsDto Options { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<MatchRegistration> Registrations { get; set; } = new List<MatchRegistration>();
  }
}
