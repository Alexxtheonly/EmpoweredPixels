using System;
using EmpoweredPixels.Attunements;

namespace EmpoweredPixels.Utilities.FighterAttunementSelector
{
  public interface IFighterAttunementSelector
  {
    AttunementBase GetAttunement(Guid? id);
  }
}
