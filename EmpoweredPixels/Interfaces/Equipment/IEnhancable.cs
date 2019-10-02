using EmpoweredPixels.Enums.Equipment;

namespace EmpoweredPixels.Interfaces.Equipment
{
  public interface IEnhancable
  {
    int Level { get; }

    ItemRarity Rarity { get; }

    int Enhancement { get; set; }
  }
}
