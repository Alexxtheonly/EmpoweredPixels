using System;

namespace EmpoweredPixels.Models.Identity
{
  public class Verification
  {
    public Guid Id { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; }
  }
}
