using System;
using System.Linq;
using AutoMapper;
using Cronos;
using EmpoweredPixels.DataTransferObjects.Leagues;
using EmpoweredPixels.Models.Leagues;
using EmpoweredPixels.Models.Matches;

namespace EmpoweredPixels.Profiles.Leagues
{
  public class LeagueProfile : Profile
  {
    public LeagueProfile()
    {
      CreateMap<League, LeagueDto>()
       .ForMember(o => o.SubscriberCount, opt => opt.MapFrom(o => o.Subscriptions.Count))
       .ForMember(o => o.IsTeam, opt => opt.MapFrom(o => o.Options.IsTeam))
       .ForMember(o => o.TeamSize, opt => opt.MapFrom(o => o.Options.TeamSize))
       .ForMember(o => o.MaxPowerlevel, opt => opt.MapFrom(o => o.Options.MatchOptions.MaxPowerlevel))
       .ForMember(o => o.NextMatch, opt => opt.MapFrom(o => CronExpression.Parse(o.Options.IntervalCron).GetNextOccurrence(DateTimeOffset.UtcNow, TimeZoneInfo.Utc, false)))
       .Include<League, LeagueDetailDto>();

      CreateMap<League, LeagueDetailDto>();

      CreateMap<LeagueSubscription, LeagueSubscriptionDto>()
        .ForMember(o => o.FighterName, opt => opt.MapFrom(o => o.Fighter.Name))
        .ForMember(o => o.User, opt => opt.MapFrom(o => o.Fighter.User.Name));

      CreateMap<LeagueSubscriptionDto, LeagueSubscription>();

      CreateMap<LeagueMatch, LeagueMatchDto>()
        .ForMember(o => o.Started, opt => opt.MapFrom(o => o.Match.Started))
        .ForMember(o => o.HasWinner, opt => opt.MapFrom(o => GetWinningFighterResult(o) == null ? false : true))
        .ForMember(o => o.WinnerFighterName, opt => opt.MapFrom(o => GetWinningFighterResult(o) == null ? string.Empty : GetWinningFighterResult(o).Fighter.Name))
        .ForMember(o => o.WinnerUser, opt => opt.MapFrom(o => GetWinningFighterResult(o) == null ? string.Empty : GetWinningFighterResult(o).Fighter.User.Name));

      CreateMap<LeagueMatch, LeagueLastWinnerDto>()
        .ForMember(o => o.Username, opt => opt.MapFrom(o => GetWinningFighterResult(o) == null ? string.Empty : GetWinningFighterResult(o).Fighter.User.Name))
        .ForMember(o => o.Fightername, opt => opt.MapFrom(o => GetWinningFighterResult(o) == null ? string.Empty : GetWinningFighterResult(o).Fighter.Name));
    }

    private static MatchContribution GetWinningFighterResult(LeagueMatch o)
    {
      return o.Match.MatchContributions.FirstOrDefault(c => c.HasWon);
    }
  }
}
