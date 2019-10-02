using System;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Interfaces.Equipment;

namespace EmpoweredPixels.DataTransferObjects.Items
{
  public class SocketStoneDto : IEnhancable
  {
    public Guid Id { get; set; }

    public Guid? EquipmentId { get; set; }

    public long UserId { get; set; }

    public SocketStat Stat { get; set; }

    public int Level { get; set; }

    public ItemRarity Rarity { get; set; }

    public int Enhancement { get; set; }
  }
}
