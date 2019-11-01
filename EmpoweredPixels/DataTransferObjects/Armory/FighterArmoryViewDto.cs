using System;

namespace EmpoweredPixels.DataTransferObjects.Armory
{
  public class FighterArmoryViewDto
  {
    public Guid FighterId { get; set; }

    public string FighterName { get; set; }

    public int FighterLevel { get; set; }

    public int FighterElo { get; set; }

    public int FighterPreviousElo { get; set; }

    public string Username { get; set; }

    public long UserId { get; set; }
  }
}
