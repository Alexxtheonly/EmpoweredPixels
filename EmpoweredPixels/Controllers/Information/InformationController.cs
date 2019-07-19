using AutoMapper;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Information;
using EmpoweredPixels.Providers.Version;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Information
{
  public class InformationController : ControllerBase<DatabaseContext, InformationController>
  {
    public InformationController(DatabaseContext context, ILogger<InformationController> logger, IMapper mapper)
      : base(context, logger, mapper)
    {
    }

    [HttpGet("version")]
    public ActionResult<VersionInformationDto> GetVersion([FromServices] IVersionProvider versionProvider)
    {
      return Ok(new VersionInformationDto()
      {
        Version = versionProvider.EmpoweredPixelsVersion,
        EngineVersion = versionProvider.SharpFightingEngineVersion,
      });
    }
  }
}
