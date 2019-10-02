using System;
using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Models.Roster;
using EmpoweredPixels.Rewards.Items;
using EmpoweredPixels.Skills.Attack;
using EmpoweredPixels.Skills.Attack.Bow.Basic;
using EmpoweredPixels.Skills.Attack.Bow.Strong;
using EmpoweredPixels.Skills.Attack.Bow.Ultimate;
using EmpoweredPixels.Skills.Attack.Greatsword.Basic;
using EmpoweredPixels.Skills.Attack.Greatsword.Strong;
using EmpoweredPixels.Skills.Attack.Greatsword.Ultimate;
using SharpFightingEngine.Skills;
using SharpFightingEngine.Skills.Heal;

namespace EmpoweredPixels.Utilities.FighterSkillSelection
{
  public class FighterSkillSelector : IFighterSkillSelector
  {
    private static readonly IEnumerable<WeaponSkillBase> WeaponSkills = new WeaponSkillBase[]
    {
      new GreatswordBlowSkill(),
      new GreatswordVortexSkill(),
      new GreatswordCuttingLeapSkill(),
      new GreatswordPowerfulStrikeSkill(),
      new GreatswordEvisceratingCutSkill(),

      new BowShotSkill(),
      new BowExplodingArrowSkill(),
      new BowPreciseArrowSkill(),
      new BowTripleArrowSkill(),
      new BowSuperSonicShotSkill(),
    };

    private static readonly IEnumerable<IHealSkill> HealSkills = new IHealSkill[]
    {
      new HealingHandHealSkill(),
    };

    public IEnumerable<ISkill> GetSkills(Fighter fighter)
    {
      if (fighter.Equipment == null)
      {
        throw new ArgumentNullException(nameof(Fighter.Equipment));
      }

      var skills = new List<ISkill>();

      var usedWeapon = fighter.Equipment.FirstOrDefault(o => EquipmentConstants.IsWeaponConstant(o.Type));
      if (usedWeapon != null)
      {
        skills.AddRange(WeaponSkills.Where(o => o.WeaponType == usedWeapon.Type));
      }

      skills.AddRange(HealSkills);

      return skills;
    }
  }
}
