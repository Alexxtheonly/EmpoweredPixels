namespace EmpoweredPixels.Extensions
{
  public static class ObjectExtension
  {
    public static long? AsLong(this object obj)
    {
      if (long.TryParse(obj?.ToString(), out long parsed))
      {
        return parsed;
      }

      return null;
    }
  }
}
