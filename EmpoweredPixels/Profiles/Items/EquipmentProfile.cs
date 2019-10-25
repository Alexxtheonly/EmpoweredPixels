using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.Models.Items;
using EmpoweredPixels.Rewards.Items;

namespace EmpoweredPixels.Profiles.Items
{
  public class EquipmentProfile : Profile
  {
    public EquipmentProfile()
    {
      CreateMap<Equipment, EquipmentDto>()
        .ForMember(o => o.IsWeapon, opt => opt.MapFrom(o => EquipmentConstants.IsWeaponConstant(o.Type)))
        .ForMember(o => o.IsFavorite, opt => opt.MapFrom(o => o.Option == null ? false : o.Option.IsFavorite));
      CreateMap<EquipmentDto, Equipment>();
    }
  }
}
