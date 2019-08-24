using System;
using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Models.Rewards
{
  public class Reward
  {
    public Guid Id { get; set; }

    public long UserId { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset? Claimed { get; set; }

    public Guid RewardPoolId { get; set; }

    public virtual User User { get; set; }
  }
}
