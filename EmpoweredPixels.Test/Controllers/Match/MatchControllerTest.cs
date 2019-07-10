using AutoMapper;
using EmpoweredPixels.Controllers.Match;
using EmpoweredPixels.Models;
using EmpoweredPixels.Profiles.Match;
using Newtonsoft.Json;
using Xunit;

namespace EmpoweredPixels.Test.Controllers.Match
{
  public class MatchControllerTest : ControllerTest<MatchController, DatabaseContext>
  {
    public MatchControllerTest()
    {
    }

    [Fact]
    public void ShouldReturnMatchDto()
    {
      var actual = TestUtilities.GetActionResultValue(Controller.GetTestMatch());
      Assert.NotNull(actual);
      Assert.NotEmpty(actual.Scores);
      Assert.NotEmpty(actual.Ticks);
    }
  }
}
