using System;
using AutoMapper;
using Cronos;
using EmpoweredPixels.DataTransferObjects.Leagues;
using EmpoweredPixels.Models.Leagues;

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
       .ForMember(o => o.NextMatch, opt => opt.MapFrom(o => CronExpression.Parse(o.Options.IntervalCron).GetNextOccurrence(DateTimeOffset.UtcNow.DateTime, false)));

      CreateMap<LeagueSubscription, LeagueSubscriptionDto>();
      CreateMap<LeagueSubscriptionDto, LeagueSubscription>();

      CreateMap<LeagueMatch, LeagueMatchDto>()
        .ForMember(o => o.Started, opt => opt.MapFrom(o => o.Match.Started));
    }
  }
}
