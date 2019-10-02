using System.Collections.Generic;
using EmpoweredPixels.Models.Roster;
using SharpFightingEngine.Skills;

namespace EmpoweredPixels.Utilities.FighterSkillSelection
{
  public interface IFighterSkillSelector
  {
    IEnumerable<ISkill> GetSkills(Fighter fighter);
  }
}
