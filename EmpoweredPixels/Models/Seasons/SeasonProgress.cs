using EmpoweredPixels.Models.Identity;

namespace EmpoweredPixels.Models.Seasons
{
  public class SeasonProgress
  {
    public long Id { get; set; }

    public int SeasonId { get; set; }

    public long UserId { get; set; }

    public int? Position { get; set; }

    public bool IsComplete { get; set; }

    public Season Season { get; set; }

    public User User { get; set; }
  }
}
