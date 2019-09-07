namespace EmpoweredPixels.Exceptions.Identity
{
  public class InvalidVerificationException : ExceptionBase
  {
    public override ErrorCode Code => ErrorCode.InvalidVerification;
  }
}
