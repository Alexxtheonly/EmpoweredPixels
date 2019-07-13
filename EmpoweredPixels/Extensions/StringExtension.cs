namespace EmpoweredPixels.Extensions
{
  public static class StringExtension
  {
    public static bool AsBoolean(this string s)
    {
      bool.TryParse(s, out bool result);

      return result;
    }

    public static int AsInteger(this string s)
    {
      int.TryParse(s, out int result);

      return result;
    }

    public static long AsLong(this string s)
    {
      long.TryParse(s, out long result);
      return result;
    }

    public static double AsDouble(this string s)
    {
      double.TryParse(s, out double result);

      return result;
    }
  }
}
