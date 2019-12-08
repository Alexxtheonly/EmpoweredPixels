using System;
using System.Collections.Generic;
using EmpoweredPixels.DataTransferObjects.Items;

namespace EmpoweredPixels.DataTransferObjects.Shop
{
  public class ShopItemDto : ItemDto
  {
    public bool IsEquipment { get; set; }

    public Guid CategoryId { get; set; }

    public IEnumerable<ShopItemPriceDto> Prices { get; set; }
  }
}
