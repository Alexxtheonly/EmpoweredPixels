using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Identity;
using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Profiles.Identity
{
  public class TokenProfile : Profile
  {
    public TokenProfile()
    {
      CreateMap<Token, TokenDto>(MemberList.Destination)
        .ForMember(o => o.Token, opt => opt.MapFrom(o => o.Value))
        .ForMember(o => o.Refresh, opt => opt.MapFrom(o => o.RefreshValue));
    }
  }
}
