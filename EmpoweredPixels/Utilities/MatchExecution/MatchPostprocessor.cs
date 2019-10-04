using System.Threading.Tasks;
using EmpoweredPixels.Models.Matches;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Utilities.MatchExecution
{
  public class MatchPostprocessor : IMatchPostprocessor
  {
    private readonly IMatchScoreProcessor scoreProcessor;
    private readonly IMatchFighterRewardTrackProcessor rewardTrackProcessor;
    private readonly IMatchFighterExperienceProcessor fighterExperienceProcessor;
    private readonly IMatchFighterEloProcessor fighterEloProcessor;
    private readonly IMatchContributionProcessor contributionProcessor;
    private readonly IMatchLogProcessor logProcessor;

    public MatchPostprocessor(
      IMatchScoreProcessor scoreProcessor,
      IMatchFighterRewardTrackProcessor rewardTrackProcessor,
      IMatchFighterExperienceProcessor fighterExperienceProcessor,
      IMatchFighterEloProcessor fighterEloProcessor,
      IMatchContributionProcessor contributionProcessor,
      IMatchLogProcessor logProcessor)
    {
      this.scoreProcessor = scoreProcessor;
      this.rewardTrackProcessor = rewardTrackProcessor;
      this.fighterExperienceProcessor = fighterExperienceProcessor;
      this.fighterEloProcessor = fighterEloProcessor;
      this.contributionProcessor = contributionProcessor;
      this.logProcessor = logProcessor;
    }

    public bool ProcessScores { get; set; } = true;

    public bool ProcessRewardTracks { get; set; } = true;

    public bool ProcessFighterExperience { get; set; } = true;

    public bool ProcessFighterElo { get; set; } = true;

    public bool ProcessFighterContribution { get; set; } = true;

    public bool ProcessLog { get; set; } = true;

    public async Task Process(Match match, IMatchResult result)
    {
      if (ProcessScores)
      {
        await scoreProcessor.Process(match, result.Scores);
      }

      if (ProcessRewardTracks)
      {
        await rewardTrackProcessor.Process(result.Contributions);
      }

      if (ProcessFighterExperience)
      {
        await fighterExperienceProcessor.Process(result.Contributions);
      }

      if (ProcessFighterElo)
      {
        await fighterEloProcessor.Process(match, result.Contributions);
      }

      if (ProcessFighterContribution)
      {
        await contributionProcessor.Process(match, result.Contributions);
      }

      if (ProcessLog)
      {
        await logProcessor.Process(match, result);
      }
    }
  }
}
