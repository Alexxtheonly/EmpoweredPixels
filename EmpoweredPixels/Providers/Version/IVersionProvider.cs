namespace EmpoweredPixels.Providers.Version
{
  public interface IVersionProvider
  {
    string EmpoweredPixelsVersion { get; }

    string SharpFightingEngineVersion { get; }
  }
}
