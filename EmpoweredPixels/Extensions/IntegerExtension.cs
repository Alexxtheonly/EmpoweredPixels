namespace EmpoweredPixels.Extensions
{
  public static class IntegerExtension
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

    /// <summary>
    /// https://en.wikipedia.org/wiki/Gauss_sum
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int GaussSum(this int value)
    {
      return (value * (value + 1)) / 2;
    }
  }
}
