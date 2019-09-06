using System;
using System.Collections.Generic;
using EmpoweredPixels.DataTransferObjects.Matches;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Factories.Matches
{
  public interface IEngineFactory
  {
    EngineCalculationValues CalculationValues { get; }

    MatchOptionsDto GetDefaultOptions();

    Engine GetEngine(IEnumerable<IFighterStats> fighters, MatchOptionsDto optionsDto);

    IEnumerable<Guid> GetAvailableBounds();
  }
}
