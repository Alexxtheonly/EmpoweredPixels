namespace EmpoweredPixels.Exceptions
{
  public enum ErrorCode : int
  {
    Undefined = 0,
    InvalidCredentials = 1,
    InvalidRefreshToken = 2,
    InvalidVerification = 3,
    InvalidLeague = 4,
    InvalidFighter = 5,
    IllegalFighterPowerlevel = 6,
    LeagueSubscriptionLimitExceeded = 7,
    InvalidLeagueSubscription = 8,
    InvalidMatch = 9,
    MatchFighterLimitExceeded = 10,
    InvalidTeam = 11,
    InvalidTeamPassword = 12,
    InvalidMatchRegistration = 13,
    InsufficientMatchRegistrations = 14,
    InvalidMatchResult = 15,
    InvalidReward = 16,
    InvalidFighterName = 17,
    InvalidEquipmentOperation = 18,
    FighterInvalidOutfit = 19,
    InsufficientEmpoweredParticles = 20,
  }
}
