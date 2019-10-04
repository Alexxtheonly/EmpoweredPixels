using System.Threading.Tasks;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Matches;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchLogProcessor : IMatchLogProcessor
  {
    private readonly DatabaseContext databaseContext;

    public MatchLogProcessor(DatabaseContext databaseContext)
    {
      this.databaseContext = databaseContext;
    }

    public async Task Process(Match match, IMatchResult result)
    {
      databaseContext.MatchResults.Add(new Models.Matches.MatchResult()
      {
        MatchId = match.Id,
        RoundTicks = JsonConvert.SerializeObject(result.Ticks.AsDto(), new JsonSerializerSettings()
        {
          ContractResolver = new CamelCasePropertyNamesContractResolver(),
        }).Compress(),
      });

      await databaseContext.SaveChangesAsync();
    }
  }
}
