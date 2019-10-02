using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Items;
using EmpoweredPixels.Models.Items;

namespace EmpoweredPixels.Profiles.Items
{
  public class SocketStoneProfile : Profile
  {
    public SocketStoneProfile()
    {
      CreateMap<SocketStone, SocketStoneDto>();
      CreateMap<SocketStoneDto, SocketStone>();
    }
  }
}
