using System;

namespace EmpoweredPixels.DataTransferObjects.Dashboard
{
  public class DashboardFighterDto
  {
    public Guid FighterId { get; set; }

    public string FighterName { get; set; }

    public int? FighterElo { get; set; }

    public int? FighterPreviousElo { get; set; }
  }
}
