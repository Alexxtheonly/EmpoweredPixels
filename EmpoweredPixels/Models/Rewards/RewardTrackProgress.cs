using System;
using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Models.Rewards
{
  public class RewardTrackProgress
  {
    public int RewardTrackId { get; set; }

    public long UserId { get; set; }

    public DateTimeOffset Activated { get; set; }

    public DateTimeOffset? Completed { get; set; }

    public long Progress { get; set; }

    public virtual User User { get; set; }

    public virtual RewardTrack RewardTrack { get; set; }
  }
}
