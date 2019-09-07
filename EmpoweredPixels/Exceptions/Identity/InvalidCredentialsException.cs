namespace EmpoweredPixels.Exceptions.Identity
{
  public class InvalidCredentialsException : ExceptionBase
  {
    public override ErrorCode Code => ErrorCode.InvalidCredentials;
  }
}
