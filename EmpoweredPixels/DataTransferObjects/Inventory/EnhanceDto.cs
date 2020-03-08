using EmpoweredPixels.DataTransferObjects.Items;

namespace EmpoweredPixels.DataTransferObjects.Inventory
{
  public class EnhanceDto
  {
    public int DesiredEnhancement { get; set; }

    public EquipmentDto Equipment { get; set; }
  }
}
