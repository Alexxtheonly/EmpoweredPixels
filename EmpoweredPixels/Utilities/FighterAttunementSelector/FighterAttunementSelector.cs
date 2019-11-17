using System;
using System.Collections.Generic;
using System.Linq;
using EmpoweredPixels.Attunements;

namespace EmpoweredPixels.Utilities.FighterAttunementSelector
{
  public class FighterAttunementSelector : IFighterAttunementSelector
  {
    private IEnumerable<AttunementBase> Attunements => new AttunementBase[]
    {
      new FireAttunement(),
      new WindAttunement(),
      new LightningAttunement(),
      new EarthAttunement(),
      new WaterAttunement(),
    };

    public AttunementBase GetAttunement(Guid? id)
    {
      return Attunements.FirstOrDefault(o => o.Id == id);
    }
  }
}
