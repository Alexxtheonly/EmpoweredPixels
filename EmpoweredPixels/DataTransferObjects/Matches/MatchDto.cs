using System;
using System.Collections.Generic;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class MatchDto
  {
    public Guid Id { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Started { get; set; }

    public bool Ended { get; set; }

    public ICollection<MatchRegistrationDto> Registrations { get; set; } = new List<MatchRegistrationDto>();

    public MatchOptionsDto Options { get; set; }
  }
}
