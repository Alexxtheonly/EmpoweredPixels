﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public interface IMatchFighterEloProcessor
  {
    Task Process(Match match, IEnumerable<FighterContribution> contributions);
  }
}
