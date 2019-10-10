using System;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Skills.Attack.Dagger
{
  public abstract class DaggerSkillBase : WeaponSkillBase
  {
    public override Guid WeaponType => EquipmentConstants.WeaponDagger;

    public override float Range => 1F;

    public override bool CanBeReflected => false;
  }
}
