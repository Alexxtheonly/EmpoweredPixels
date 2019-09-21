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

      CreateMap<MatchScoreFighter, MatchScoreFighterDto>()
        .ForMember(o => o.FighterId, opt => opt.MapFrom(o => o.FighterId))
        .ForMember(o => o.FighterName, opt => opt.MapFrom(o => o.Fighter.Name))
        .ForMember(o => o.Username, opt => opt.MapFrom(o => o.Fighter.User.Name))
        .ForMember(o => o.RoundsAlive, opt => opt.MapFrom(o => o.RoundsAlive))
        .ForMember(o => o.TeamId, opt => opt.MapFrom(o => o.TeamId))
        .ForMember(o => o.TotalDamageDone, opt => opt.MapFrom(o => o.TotalDamageDone))
        .ForMember(o => o.TotalDamageTaken, opt => opt.MapFrom(o => o.TotalDamageTaken))
        .ForMember(o => o.TotalDeaths, opt => opt.MapFrom(o => o.TotalDeaths))
        .ForMember(o => o.TotalDistanceTraveled, opt => opt.MapFrom(o => o.TotalDistanceTraveled))
        .ForMember(o => o.TotalEnergyUsed, opt => opt.MapFrom(o => o.TotalEnergyUsed))
        .ForMember(o => o.TotalKills, opt => opt.MapFrom(o => o.TotalKills))
        .ForMember(o => o.TotalRegeneratedEnergy, opt => opt.MapFrom(o => o.TotalRegeneratedEnergy))
        .ForMember(o => o.TotalRegeneratedHealth, opt => opt.MapFrom(o => o.TotalRegeneratedHealth));
    }
  }
}
