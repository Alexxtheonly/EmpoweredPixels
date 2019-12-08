using System;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Utilities.Paging;

namespace EmpoweredPixels.Controllers.Shop
{
  public class ShopFilterDto : PagingOptions
  {
    public Guid? CategoryId { get; set; }

    public ItemRarity? Rarity { get; set; }
  }
}
