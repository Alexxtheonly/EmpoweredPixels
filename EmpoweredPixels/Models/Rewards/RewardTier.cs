using System;

namespace EmpoweredPixels.Models.Rewards
{
  public class RewardTier
  {
    public int Id { get; set; }

    public int RewardTrackId { get; set; }

    public int Points { get; set; }

    public Guid RewardPoolId { get; set; }

    public virtual RewardTrack RewardTrack { get; set; }
  }
}
