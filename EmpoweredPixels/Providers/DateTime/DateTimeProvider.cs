using System;

namespace EmpoweredPixels.Providers.DateTime
{
  public class DateTimeProvider : IDateTimeProvider
  {
    public DateTimeOffset Now => DateTimeOffset.UtcNow;
  }
}
