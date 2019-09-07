using System;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Identity;
using EmpoweredPixels.Exceptions.Identity;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpoweredPixels.Controllers.Identity
{
  public class RegisterController : ControllerBase<DatabaseContext, RegisterController>
  {
    private readonly IDateTimeProvider dateTimeProvider;

    public RegisterController(DatabaseContext context, ILogger<RegisterController> logger, IMapper mapper, IDateTimeProvider dateTimeProvider)
      : base(context, logger, mapper)
    {
      this.dateTimeProvider = dateTimeProvider;
    }

    [HttpPost]
    public async Task<ActionResult> Register([FromBody] RegisterDto register)
    {
      var user = new User()
      {
        Name = register.Username,
        Email = register.Email,
        Created = dateTimeProvider.Now,
      };
      user.SetPassword(register.Password);

      Context.Verifications.Add(new Verification()
      {
        User = user,
      });

      await Context.SaveChangesAsync();

      return Ok();
    }

    [HttpPost("verify")]
    public async Task<ActionResult> Verify([FromBody] Guid value)
    {
      var verification = await Context.Verifications
          .FirstOrDefaultAsync(o => o.Id == value);

      if (verification == null)
      {
        return BadRequest(new InvalidVerificationException());
      }

      Context.Verifications.Remove(verification);
      await Context.SaveChangesAsync();

      return Ok();
    }
  }
}
