using System;
using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchExecutor : IMatchExecutor
  {
    private readonly IMatchFighterPreparer fighterPreparer;
    private readonly IMatchProcessor processor;
    private readonly IMatchPostprocessor postprocessor;
    private readonly IDateTimeProvider dateTimeProvider;

    public MatchExecutor(
      IMatchFighterPreparer fighterPreparer,
      IMatchProcessor processor,
      IMatchPostprocessor postprocessor,
      IDateTimeProvider dateTimeProvider)
    {
      this.fighterPreparer = fighterPreparer;
      this.processor = processor;
      this.postprocessor = postprocessor;
      this.dateTimeProvider = dateTimeProvider;
    }

    public bool ProcessScores
    {
      get => postprocessor.ProcessScores;
      set => postprocessor.ProcessScores = value;
    }

    public bool ProcessRewardTracks
    {
      get => postprocessor.ProcessRewardTracks;
      set => postprocessor.ProcessRewardTracks = value;
    }

    public bool ProcessFighterExperience
    {
      get => postprocessor.ProcessFighterExperience;
      set => postprocessor.ProcessFighterExperience = value;
    }

    public bool ProcessFighterElo
    {
      get => postprocessor.ProcessFighterElo;
      set => postprocessor.ProcessFighterElo = value;
    }

    public bool ProcessFighterContribution
    {
      get => postprocessor.ProcessFighterContribution;
      set => postprocessor.ProcessFighterContribution = value;
    }

    public bool ProcessLog
    {
      get => postprocessor.ProcessLog;
      set => postprocessor.ProcessLog = value;
    }

    public async Task Execute(Match match)
    {
      if (match.Id == default)
      {
        throw new ArgumentException("Match id must be set");
      }

      if (!match.Registrations.Skip(1).Any())
      {
        return;
      }

      match.Started = dateTimeProvider.Now;

      var fighters = fighterPreparer.GetFighters(match);

      var result = await processor.Process(fighters, match);
      await postprocessor.Process(match, result);
    }
  }
}
