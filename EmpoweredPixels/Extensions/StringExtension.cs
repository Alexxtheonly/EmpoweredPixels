using System;
using System.IO;
using System.IO.Compression;
using System.Text;

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

    public static string Compress(this string s)
    {
      var bytes = Encoding.Unicode.GetBytes(s);
      using (var memoryStreamIn = new MemoryStream(bytes))
      using (var memoryStreamOut = new MemoryStream())
      {
        using (var gs = new GZipStream(memoryStreamOut, CompressionMode.Compress))
        {
          memoryStreamIn.CopyTo(gs);
        }

        return Convert.ToBase64String(memoryStreamOut.ToArray());
      }
    }

    public static string Decompress(this string s)
    {
      var bytes = Convert.FromBase64String(s);
      using (var memoryStreamIn = new MemoryStream(bytes))
      using (var memoryStreamOut = new MemoryStream())
      {
        using (var gs = new GZipStream(memoryStreamIn, CompressionMode.Decompress))
        {
          gs.CopyTo(memoryStreamOut);
        }

        return Encoding.Unicode.GetString(memoryStreamOut.ToArray());
      }
    }
  }
}
