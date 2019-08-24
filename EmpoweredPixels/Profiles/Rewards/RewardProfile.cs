using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Rewards;
using EmpoweredPixels.Models.Rewards;

namespace EmpoweredPixels.Profiles.Rewards
{
  public class RewardProfile : Profile
  {
    public RewardProfile()
    {
      CreateMap<Reward, RewardDto>()
        .ForMember(o => o.Id, opt => opt.MapFrom(o => o.Id))
        .ForMember(o => o.PoolId, opt => opt.MapFrom(o => o.RewardPoolId));
    }
  }
}
