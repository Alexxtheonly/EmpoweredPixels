using System;
using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Models.Items
{
  public class Item
  {
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }
  }
}
