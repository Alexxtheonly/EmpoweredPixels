using System;

namespace EmpoweredPixels.Extensions
{
  public static class VersionExtension
  {
    public static string GetFormatted(this Version version)
    {
      return $"{version.Major}.{version.Minor}.{version.Build}";
    }
  }
}
