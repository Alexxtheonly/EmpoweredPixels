using System;

namespace EmpoweredPixels.Providers.DateTime
{
  public interface IDateTimeProvider
  {
    DateTimeOffset Now { get; }
  }
}
