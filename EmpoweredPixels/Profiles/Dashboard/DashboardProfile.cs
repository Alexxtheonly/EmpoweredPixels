using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Dashboard;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Profiles.Dashboard
{
  public class DashboardProfile : Profile
  {
    public DashboardProfile()
    {
      CreateMap<Fighter, DashboardFighterDto>()
        .ForMember(o => o.FighterId, opt => opt.MapFrom(o => o.Id))
        .ForMember(o => o.FighterName, opt => opt.MapFrom(o => o.Name))
        .ForMember(o => o.FighterElo, opt => opt.MapFrom(o => o.EloRating.CurrentElo))
        .ForMember(o => o.FighterPreviousElo, opt => opt.MapFrom(o => o.EloRating.PreviousElo));
    }
  }
}
