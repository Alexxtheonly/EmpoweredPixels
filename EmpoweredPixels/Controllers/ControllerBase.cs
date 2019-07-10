using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers
{
  [Route("api/[controller]")]
  public class ControllerBase<TContext, TController> : Controller
    where TContext : DbContext
  {
    public ControllerBase(IHostingEnvironment environment)
    {
      Environment = environment;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ControllerBase{T, C}"/> class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public ControllerBase(TContext context, ILogger<TController> logger, IMapper mapper)
    {
      Context = context;
      Logger = logger;
      Mapper = mapper;
    }

    /// <summary>
    /// Context to access the database.
    /// </summary>
    protected TContext Context { get; private set; }

    /// <summary>
    /// Logger used to log events
    /// </summary>
    protected ILogger<TController> Logger { get; private set; }

    /// <summary>
    /// The environment
    /// </summary>
    protected IHostingEnvironment Environment { get; private set; }

    /// <summary>
    /// Dto mapper
    /// </summary>
    protected IMapper Mapper { get; private set; }
  }
}
