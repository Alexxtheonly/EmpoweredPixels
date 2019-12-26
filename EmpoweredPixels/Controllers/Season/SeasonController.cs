using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmpoweredPixels.DataTransferObjects.Season;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Utilities.Paging;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Season
{
  public class SeasonController : ControllerBase<DatabaseContext, SeasonController>
  {
    public SeasonController(DatabaseContext context, ILogger<SeasonController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpPost("summary")]
    public async Task<ActionResult<Page<SeasonSummaryDto>>> GetSummaryPage([FromBody] PagingOptions options)
    {
      var userId = User.Claims.GetUserId();
      if (userId == null)
      {
        return Forbid();
      }

      return Ok(await Context.SeasonSummaries
        .Where(o => o.UserId == userId)
        .OrderByDescending(o => o.SeasonId)
        .ProjectTo<SeasonSummaryDto>(Mapper.ConfigurationProvider)
        .GetPage(options));
    }
  }
}
