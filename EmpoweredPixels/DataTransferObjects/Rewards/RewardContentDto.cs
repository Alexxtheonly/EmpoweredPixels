using System.Collections.Generic;
using EmpoweredPixels.DataTransferObjects.Items;

namespace EmpoweredPixels.DataTransferObjects.Rewards
{
  public class RewardContentDto
  {
    public ICollection<ItemDto> Items { get; set; } = new List<ItemDto>();

    public ICollection<EquipmentDto> Equipment { get; set; } = new List<EquipmentDto>();
  }
}
