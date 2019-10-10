using System;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Skills.Attack.Bow
{
  public abstract class BowSkillBase : WeaponSkillBase
  {
    /// <summary>
    /// Based on a few hunting sites the effective (kill) range is somewhere between 18m and 36m depending on the archer.
    /// </summary>
    public override float Range => 27F;

    public override bool CanBeReflected => true;

    public override Guid WeaponType => EquipmentConstants.WeaponBow;
  }
}
