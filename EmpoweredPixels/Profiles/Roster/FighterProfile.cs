using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Armory;
using EmpoweredPixels.DataTransferObjects.Roster;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Profiles.Roster
{
  public class FighterProfile : Profile
  {
    public FighterProfile()
    {
      CreateMap<Fighter, FighterDto>(MemberList.Destination)
        .ForMember(o => o.Equipment, opt => opt.MapFrom(o => o.Equipment));

      CreateMap<Fighter, FighterNameDto>(MemberList.Destination);

      CreateMap<Fighter, FighterArmoryViewDto>()
        .ForMember(o => o.FighterId, opt => opt.MapFrom(o => o.Id))
        .ForMember(o => o.FighterLevel, opt => opt.MapFrom(o => o.Level))
        .ForMember(o => o.FighterName, opt => opt.MapFrom(o => o.Name))
        .ForMember(o => o.Username, opt => opt.MapFrom(o => o.User.Name))
        .ForMember(o => o.UserId, opt => opt.MapFrom(o => o.UserId));
    }
  }
}
