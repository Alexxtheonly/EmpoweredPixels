using AutoMapper;
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
    }
  }
}
