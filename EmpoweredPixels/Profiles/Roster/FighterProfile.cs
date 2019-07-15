using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Roster;
using EmpoweredPixels.Models.Roster;

namespace EmpoweredPixels.Profiles.Roster
{
  public class FighterProfile : Profile
  {
    public FighterProfile()
    {
      CreateMap<Fighter, FighterDto>(MemberList.Destination);
      CreateMap<FighterDto, Fighter>(MemberList.Source);

      CreateMap<Fighter, FighterNameDto>(MemberList.Destination);
    }
  }
}
