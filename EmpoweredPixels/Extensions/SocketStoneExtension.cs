using EmpoweredPixels.Models.Items;

namespace EmpoweredPixels.Extensions
{
  public static class SocketStoneExtension
  {
    public static int GetValue(this SocketStone socketStone)
    {
      return (int)socketStone.Rarity + socketStone.Enhancement;
    }
  }
}
