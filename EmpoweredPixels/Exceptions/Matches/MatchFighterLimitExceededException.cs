namespace EmpoweredPixels.Exceptions.Matches
{
  public class MatchFighterLimitExceededException : ExceptionBase
  {
    public override ErrorCode Code => ErrorCode.MatchFighterLimitExceeded;
  }
}
