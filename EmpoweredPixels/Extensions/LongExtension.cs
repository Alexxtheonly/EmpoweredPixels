namespace EmpoweredPixels.Extensions
{
  public static class LongExtension
  {
    public static long NearestBase(this long value, int @base)
    {
      var result = value - (value % @base);
      return result;
    }

    public static int NearestBase(this int value, int @base)
    {
      var result = value - (value % @base);
      return result;
    }
  }
}
