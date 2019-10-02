using System;
using System.Collections.Generic;
using System.Linq;

namespace EmpoweredPixels.Rewards.Items
{
  public static class EquipmentConstants
  {
    public static readonly Guid ArmorHead = new Guid("4408EBB1-9213-45C2-9E6D-8AD22EAD4EDC");

    public static readonly Guid ArmorShoulders = new Guid("47B22820-C3D7-495B-AAC8-F29501623723");

    public static readonly Guid ArmorChest = new Guid("71E105F2-A098-47D7-A2DE-561CA434EC54");

    public static readonly Guid ArmorHands = new Guid("640D7E83-9D98-471E-928F-5A4EDAA7F4B0");

    public static readonly Guid ArmorLegs = new Guid("922B5587-94E0-4FD1-A896-8E3F64C71304");

    public static readonly Guid ArmorShoes = new Guid("724BC2A7-712A-45C5-8C01-BB9C5582E33B");

    public static readonly Guid WeaponGreatsword = new Guid("96753286-FD45-489E-A3FB-25DC758F94F7");

    public static readonly Guid WeaponBow = new Guid("FB6D6951-7630-4AD8-A005-DF72BC02C3BF");

    private static readonly IEnumerable<Guid> Weapons = new Guid[]
    {
      WeaponBow,
      WeaponGreatsword,
    };

    public static bool IsWeaponConstant(Guid equipment)
    {
      return Weapons.Contains(equipment);
    }
  }
}
