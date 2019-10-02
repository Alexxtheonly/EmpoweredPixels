using System.Linq;
using System.Threading.Tasks;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Factories.Matches;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Matches;
using EmpoweredPixels.Providers.DateTime;
using EmpoweredPixels.Utilities.ContributionPointCalculation;
using EmpoweredPixels.Utilities.EloCalculation;
using EmpoweredPixels.Utilities.FighterProgress;
using EmpoweredPixels.Utilities.FighterSkillSelection;
using EmpoweredPixels.Utilities.FighterStatCalculation;
using EmpoweredPixels.Utilities.RewardTrackCalculation;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Jobs
{
  public class LeagueJob : ILeagueJob
  {
    private readonly DatabaseContext context;
    private readonly IEngineFactory engineFactory;
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly IContributionPointCalculator contributionPointCalculator;
    private readonly IEloCalculator eloCalculator;
    private readonly IFighterExperienceCalculator fighterExperienceCalculator;
    private readonly IFighterLevelUpHandler fighterLevelUpHandler;
    private readonly IFighterStatCalculator fighterStatCalculator;
    private readonly IFighterSkillSelector fighterSkillSelector;
    private readonly IRewardTrackCalculator rewardTrackCalculator;

    public LeagueJob(
      DatabaseContext context,
      IEngineFactory engineFactory,
      IDateTimeProvider dateTimeProvider,
      IContributionPointCalculator contributionPointCalculator,
      IEloCalculator eloCalculator,
      IFighterExperienceCalculator fighterExperienceCalculator,
      IFighterLevelUpHandler fighterLevelUpHandler,
      IFighterStatCalculator fighterStatCalculator,
      IFighterSkillSelector fighterSkillSelector,
      IRewardTrackCalculator rewardTrackCalculator)
    {
      this.context = context;
      this.engineFactory = engineFactory;
      this.dateTimeProvider = dateTimeProvider;
      this.contributionPointCalculator = contributionPointCalculator;
      this.eloCalculator = eloCalculator;
      this.fighterExperienceCalculator = fighterExperienceCalculator;
      this.fighterLevelUpHandler = fighterLevelUpHandler;
      this.fighterStatCalculator = fighterStatCalculator;
      this.fighterSkillSelector = fighterSkillSelector;
      this.rewardTrackCalculator = rewardTrackCalculator;
    }

    public async Task RunMatchAsync(int leagueId)
    {
      var league = await context.Leagues
        .Where(o => !o.IsDeactivated)
        .Include(o => o.Subscriptions)
        .ThenInclude(o => o.Fighter)
        .FirstOrDefaultAsync(o => o.Id == leagueId);

      if (league == null)
      {
        return;
      }

      var match = new Match()
      {
        Created = dateTimeProvider.Now,
        Options = league.Options.MatchOptions,
      };

      foreach (var subscriber in league.Subscriptions)
      {
        match.Registrations.Add(new MatchRegistration()
        {
          Date = dateTimeProvider.Now,
          FighterId = subscriber.FighterId,
          Match = match,
        });
      }

      if (!match.Registrations.Skip(1).Any())
      {
        return;
      }

      context.Add(match);
      await context.SaveChangesAsync();

      match = await context.Matches
        .AsTracking()
        .Include(o => o.Registrations)
        .ThenInclude(o => o.Fighter)
        .ThenInclude(o => o.Equipment)
        .ThenInclude(o => o.SocketStones)
        .FirstOrDefaultAsync(o => o.Id == match.Id);

      context.Add(new LeagueMatch()
      {
        LeagueId = league.Id,
        MatchId = match.Id,
      });

      await context.StartMatch(
        match,
        dateTimeProvider,
        engineFactory,
        contributionPointCalculator,
        eloCalculator,
        fighterExperienceCalculator,
        fighterLevelUpHandler,
        fighterStatCalculator,
        fighterSkillSelector,
        rewardTrackCalculator,
        true);

      await context.SaveChangesAsync();
    }
  }
}
