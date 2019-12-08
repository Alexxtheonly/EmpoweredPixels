using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.Models.Items;

namespace EmpoweredPixels.Profiles.Items
{
  public class ItemProfile : Profile
  {
    public ItemProfile()
    {
      CreateMap<Item, ItemDto>()
        .ForMember(o => o.Id, opt => opt.MapFrom(o => o.Id))
        .ForMember(o => o.ItemId, opt => opt.MapFrom(o => o.ItemId))
        .ForMember(o => o.Rarity, opt => opt.MapFrom(o => o.Rarity));
    }
  }
}
