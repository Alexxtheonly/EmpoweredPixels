using System.Collections.Generic;

namespace EmpoweredPixels.DataTransferObjects.Match
{
  public class MatchDto
  {
    public IEnumerable<RoundTickDto> Ticks { get; set; }

    public IEnumerable<MatchScoreDto> Scores { get; set; }

    public IEnumerable<MatchScoreDto> TeamScores { get; set; }
  }
}
