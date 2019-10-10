using System;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Skills.Attack.Greatsword
{
  public abstract class GreatswordSkillBase : WeaponSkillBase
  {
    /// <summary>
    /// Avg. Greatsword length was 150cm + ~40cm arm length
    /// </summary>
    public override float Range => 1.9F;

    public override bool CanBeReflected => false;

    public override Guid WeaponType => EquipmentConstants.WeaponGreatsword;
  }
}
