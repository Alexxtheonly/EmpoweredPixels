using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Season;
using EmpoweredPixels.Models.Seasons;

namespace EmpoweredPixels.Profiles.Season
{
  public class SeasonProfile : Profile
  {
    public SeasonProfile()
    {
      CreateMap<SeasonSummary, SeasonSummaryDto>()
        .ForMember(o => o.SeasonId, opt => opt.MapFrom(o => o.Season.SeasonId))
        .ForMember(o => o.SalvageValue, opt => opt.MapFrom(o => o.SalvageValue))
        .ForMember(o => o.Bonus, opt => opt.MapFrom(o => o.Bonus))
        .ForMember(o => o.Position, opt => opt.MapFrom(o => o.Position));
    }
  }
}
