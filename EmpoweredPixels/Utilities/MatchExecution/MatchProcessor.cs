using System.Collections.Generic;
using System.Threading.Tasks;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Engines;
using SharpFightingEngine.Fighters;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchProcessor : IMatchProcessor
  {
    private readonly IEngineFactory engineFactory;

    public MatchProcessor(IEngineFactory engineFactory)
    {
      this.engineFactory = engineFactory;
    }

    public async Task<IMatchResult> Process(IEnumerable<FighterBase> fighters, Match match)
    {
      var engine = engineFactory.GetEngine(fighters, match.Options);
      return await Task.Run(() => engine.StartMatch());
    }
  }
}
