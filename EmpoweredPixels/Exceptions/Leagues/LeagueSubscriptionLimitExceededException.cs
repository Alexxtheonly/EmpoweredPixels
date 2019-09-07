namespace EmpoweredPixels.Exceptions.Leagues
{
  public class LeagueSubscriptionLimitExceededException : ExceptionBase
  {
    public override ErrorCode Code => ErrorCode.LeagueSubscriptionLimitExceeded;
  }
}
