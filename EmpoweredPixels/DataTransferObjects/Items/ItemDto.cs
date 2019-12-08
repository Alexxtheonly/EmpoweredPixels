using System;
using EmpoweredPixels.Enums.Equipment;

namespace EmpoweredPixels.DataTransferObjects.Items
{
  public class ItemDto
  {
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public ItemRarity Rarity { get; set; }
  }
}
