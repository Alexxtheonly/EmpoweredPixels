namespace EmpoweredPixels.DataTransferObjects.Items
{
  public class EquipmentFilterDto
  {
    public bool OnlyUnequipped { get; set; }

    public bool ShowWeapons { get; set; }

    public bool ShowArmorHead { get; set; }

    public bool ShowArmorShoulders { get; set; }

    public bool ShowArmorChest { get; set; }

    public bool ShowArmorHands { get; set; }

    public bool ShowArmorLegs { get; set; }

    public bool ShowArmorShoes { get; set; }

    public int? MaxLevel { get; set; }
  }
}
