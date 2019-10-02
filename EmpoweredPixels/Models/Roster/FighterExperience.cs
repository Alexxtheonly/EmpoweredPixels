using System;

namespace EmpoweredPixels.Models.Roster
{
  public class FighterExperience
  {
    public Guid Id { get; set; }

    public Guid FighterId { get; set; }

    public long Points { get; set; }

    public DateTimeOffset? LastUpdate { get; set; }

    public virtual Fighter Fighter { get; set; }
  }
}
