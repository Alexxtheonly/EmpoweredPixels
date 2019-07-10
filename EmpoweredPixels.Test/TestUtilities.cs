using EmpoweredPixels.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace EmpoweredPixels.Test
{
  public static class TestUtilities
  {
    public static T GetActionResultValue<T>(IActionResult result)
    {
      var value = Assert.IsType<OkObjectResult>(result).Value;
      return Assert.IsAssignableFrom<T>(value);
    }

    /// <summary>
    /// Returns the result of the <see cref="OkObjectResult"/>
    /// </summary>
    /// <typeparam name="T">The expected return type</typeparam>
    /// <returns></returns>
    public static T GetActionResultValue<T>(ActionResult<T> result)
    {
      var value = Assert.IsType<OkObjectResult>(result.Result).Value;
      return Assert.IsAssignableFrom<T>(value);
    }

    public static IHostingEnvironment GetHostingEnvironment()
    {
      return Mock.Of<IHostingEnvironment>();
    }

    public static T GetTestContext<T>()
      where T : DbContext
    {
      var options = (DbContextOptions<T>)new DbContextOptionsBuilder<T>().ConfigureInMemoryDatabase().Options;

      var constructor = typeof(T)
        .GetConstructors()
        .First(c => c.GetParameters().Count() == 1);

      return (T)constructor.Invoke(new object[] { options });
    }

  }
}
