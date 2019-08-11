using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Matches;
using EmpoweredPixels.Models.Matches;

namespace EmpoweredPixels.Profiles.Matches
{
  public class MatchProfile : Profile
  {
    public MatchProfile()
    {
      CreateMap<Match, MatchDto>()
        .ForMember(o => o.Ended, opt => opt.MapFrom(o => o.Started != null));

      CreateMap<MatchRegistration, MatchRegistrationDto>()
        .ForMember(o => o.FighterName, opt => opt.MapFrom(o => o.Fighter.Name))
        .ForMember(o => o.Username, opt => opt.MapFrom(o => o.Fighter.User.Name))
        .ForMember(o => o.Joined, opt => opt.MapFrom(o => o.Date));

      CreateMap<MatchTeam, MatchTeamDto>()
        .ForMember(o => o.HasPassword, opt => opt.MapFrom(o => !string.IsNullOrEmpty(o.Password)));

      CreateMap<MatchFighterResult, MatchFighterResultDto>();
    }
  }
}
