using System;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Interfaces.Equipment;
using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Models.Items
{
  public class SocketStone : IEnhancable
  {
    public Guid Id { get; set; }

    public Guid? EquipmentId { get; set; }

    public long UserId { get; set; }

    public int Level { get; set; }

    public ItemRarity Rarity { get; set; }

    public int Enhancement { get; set; }

    public SocketStat Stat { get; set; }

    public virtual User User { get; set; }

    public virtual Equipment Equipment { get; set; }
  }
}
