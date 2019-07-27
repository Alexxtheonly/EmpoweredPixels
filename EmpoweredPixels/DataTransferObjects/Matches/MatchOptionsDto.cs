using System;
using System.Collections.Generic;

namespace EmpoweredPixels.DataTransferObjects.Matches
{
  public class MatchOptionsDto
  {
    public int? MaxPowerlevel { get; set; }

    public int ActionsPerRound { get; set; }

    public int? MaxFightersPerUser { get; set; }

    public int? BotCount { get; set; }

    public int? BotPowerlevel { get; set; }

    public ICollection<Guid> Features { get; set; }

    public Guid Battlefield { get; set; }

    public Guid Bounds { get; set; }

    public Guid PositionGenerator { get; set; }

    public Guid MoveOrder { get; set; }

    public Guid WinCondition { get; set; }

    public Guid StaleCondition { get; set; }
  }
}
