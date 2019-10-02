using System;

namespace EmpoweredPixels.Exceptions
{
  public abstract class ExceptionBase : Exception
  {
    public abstract ErrorCode Code { get; }

    public override string Message { get; }
  }
}
