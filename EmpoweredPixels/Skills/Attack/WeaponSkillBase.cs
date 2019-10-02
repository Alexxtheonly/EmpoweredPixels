using System;
using SharpFightingEngine.Skills.General;

namespace EmpoweredPixels.Skills.Attack
{
  public abstract class WeaponSkillBase : DamageSkillBase, IWeaponSkill
  {
    public abstract Guid WeaponType { get; }
  }
}
