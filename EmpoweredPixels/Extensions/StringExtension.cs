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

    public static byte[] Compress(this string s)
    {
      var bytes = Encoding.Unicode.GetBytes(s);
      using (var memoryStreamIn = new MemoryStream(bytes))
      using (var memoryStreamOut = new MemoryStream())
      {
        using (var compressionStream = new BrotliStream(memoryStreamOut, CompressionLevel.Optimal))
        {
          memoryStreamIn.CopyTo(compressionStream);
        }

        return memoryStreamOut.ToArray();
      }
    }

    public static string Decompress(this byte[] bytes)
    {
      using (var memoryStreamIn = new MemoryStream(bytes))
      using (var memoryStreamOut = new MemoryStream())
      {
        using (var decompressionStream = new BrotliStream(memoryStreamIn, CompressionMode.Decompress))
        {
          decompressionStream.CopyTo(memoryStreamOut);
        }

        return Encoding.Unicode.GetString(memoryStreamOut.ToArray());
      }
    }
  }
}
