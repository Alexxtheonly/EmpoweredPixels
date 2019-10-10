using System;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Skills.Attack.Gaive
{
  public abstract class GlaiveSkillBase : WeaponSkillBase
  {
    public override Guid WeaponType => EquipmentConstants.WeaponGlaive;

    public override bool CanBeReflected => false;

    public override float Range => 3.3F;
  }
}
