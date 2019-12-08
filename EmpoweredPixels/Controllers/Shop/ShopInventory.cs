using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.DataTransferObjects.Shop;
using EmpoweredPixels.Enums.Equipment;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Controllers.Shop
{
  public class ShopInventory
  {
    public static readonly IEnumerable<ShopItemDto> Items = BuildEquipmentShopItems();

    private static IEnumerable<ShopItemDto> BuildEquipmentShopItems()
    {
      foreach (var equipment in EquipmentConstants.Weapons.Union(EquipmentConstants.Armors))
      {
        foreach (var rarity in new ItemRarity[]
        {
          ItemRarity.Common,
          ItemRarity.Rare,
          ItemRarity.Fabled,
          ItemRarity.Mythic,
        })
        {
          yield return new ShopItemDto()
          {
            ItemId = equipment,
            Rarity = rarity,
            IsEquipment = true,
            Prices = new ShopItemPriceDto[]
            {
              new ShopItemPriceDto()
              {
                CurrencyItemId = EquipmentToken.Get(rarity),
                Quantity = 1,
              },
              new ShopItemPriceDto()
              {
                CurrencyItemId = EmpoweredParticle.Id,
                Quantity = (int)rarity * 30,
              }
            },
          };
        }
      }
    }
  }
}
