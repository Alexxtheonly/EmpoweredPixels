using System;

namespace EmpoweredPixels.Models.Items
{
  public class EquipmentOption
  {
    public Guid EquipmentId { get; set; }

    public bool IsFavorite { get; set; }

    public Equipment Equipment { get; set; }
  }
}
