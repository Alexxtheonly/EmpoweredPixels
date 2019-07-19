using EmpoweredPixels.Extensions;
using SharpFightingEngine.Engines;

namespace EmpoweredPixels.Providers.Version
{
  public class VersionProvider : IVersionProvider
  {
    public string EmpoweredPixelsVersion { get; private set; } = typeof(VersionProvider)
      .Assembly
      .GetName()
      .Version
      .GetFormatted();

    public string SharpFightingEngineVersion { get; private set; } = typeof(Engine)
      .Assembly
      .GetName()
      .Version
      .GetFormatted();
  }
}
