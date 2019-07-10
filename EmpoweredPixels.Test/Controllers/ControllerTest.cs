using AutoMapper;
using EmpoweredPixels.Controllers;
using EmpoweredPixels.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmpoweredPixels.Test.Controllers
{
  /// <summary>
  /// A abstract test class with already configured in memory db context.
  /// </summary>
  public abstract class ControllerTest<TController, TDbContext>
    where TDbContext : DbContext
    where TController : ControllerBase<TDbContext, TController>
  {
    public ControllerTest()
    {
      Context = TestUtilities.GetTestContext<TDbContext>();
      InitializeMapper();
      InitializeController();
      PopulateContext(Context);
    }

    protected TDbContext Context { get; private set; }

    protected TController Controller { get; private set; }

    protected IMapper Mapper { get; private set; }

    /// <summary>
    /// After the MapperConfiguration has been set, the following methods must be executed to apply the changes.
    /// <para></para>
    /// <see cref="InitializeMapper"/>
    /// <para></para>
    /// <see cref="InitializeController(object[])"/>
    /// </summary>
    protected MapperConfiguration MapperConfiguration { get; set; }

    private IHostingEnvironment HostingEnvironment => TestUtilities.GetHostingEnvironment();

    /// <summary>
    /// Use this method to populate the <see cref="Context"/> with your test data
    /// </summary>
    protected virtual void PopulateContext(TDbContext context)
    {
    }

    protected void InitializeController(params object[] args)
    {
      // Union with default parameters
      var objects = args.Union(new object[] { Context, HostingEnvironment, Mapper }).ToArray();

      Controller = BetterActivator.CreateInstance<TController>(objects);
    }

    protected virtual void InitializeMapper()
    {
      Mapper = (MapperConfiguration ?? new MapperConfiguration(cfg =>
      {
      }))
        .CreateMapper();

      Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
  }
}
