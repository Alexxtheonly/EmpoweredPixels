namespace EmpoweredPixels.Exceptions.Identity
{
  public class InvalidRefreshTokenException : ExceptionBase
  {
    public override ErrorCode Code => ErrorCode.InvalidRefreshToken;
  }
}
