using System.Collections.Generic;

namespace EmpoweredPixels.DataTransferObjects.Match
{
  public class RoundTickDto : TickDto
  {
    public int Round { get; set; }

    public IEnumerable<TickDto> Ticks { get; set; }

    public IEnumerable<RoundScoreDto> Scores { get; set; }
  }
}
