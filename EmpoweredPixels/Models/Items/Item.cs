﻿using System;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Rewards;

namespace EmpoweredPixels.Models.Items
{
  public class Item : IReward
  {
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }
  }
}
