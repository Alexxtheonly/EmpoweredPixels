using System.Collections.Generic;

namespace EmpoweredPixels.Models.Rewards
{
  public class RewardTrack
  {
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public int TotalPoints { get; set; }

    public IEnumerable<RewardTier> Tiers { get; set; }
  }
}
