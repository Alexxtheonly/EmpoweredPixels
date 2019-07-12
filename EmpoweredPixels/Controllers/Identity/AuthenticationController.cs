using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EmpoweredPixels.DataTransferObjects.Identity;
using EmpoweredPixels.Extensions;
using EmpoweredPixels.Models;
using EmpoweredPixels.Models.Identity;
using EmpoweredPixels.Providers.DateTime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace EmpoweredPixels.Controllers.Identity
{
  public class AuthenticationController : ControllerBase<DatabaseContext, AuthenticationController>
  {
    private readonly IConfiguration configuration;
    private readonly IDateTimeProvider dateTimeProvider;

    public AuthenticationController(
      DatabaseContext context,
      ILogger<AuthenticationController> logger,
      IMapper mapper,
      IConfiguration configuration,
      IDateTimeProvider dateTimeProvider)
      : base(context, logger, mapper)
    {
      this.configuration = configuration;
      this.dateTimeProvider = dateTimeProvider;
    }

    [HttpPost("token")]
    public async Task<ActionResult<TokenDto>> GetToken([FromBody] LoginDto login)
    {
      // allow login with name and email
      var user = await Context.Users
        .Where(o => o.IsVerified)
        .SingleOrDefaultAsync(o => o.Name == login.User || o.Email == login.User);

      if (user == null || !user.IsValidPassword(login.Password))
      {
        return BadRequest();
      }

      var token = await Context.Tokens
        .AsTracking()
        .Include(o => o.User)
        .FirstOrDefaultAsync(o => o.UserId == user.Id);

      if (token == null)
      {
        token = new Token()
        {
          UserId = user.Id,
          User = user,
        };
      }

      return await GetToken(token);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenDto>> GetRefreshedToken([FromBody] TokenDto tokenDto)
    {
      var token = await Context.Tokens
        .AsTracking()
        .Include(o => o.User)
        .SingleOrDefaultAsync(o => o.UserId == tokenDto.UserId && o.RefreshValue == tokenDto.Refresh);

      if (token == null)
      {
        return BadRequest();
      }

      return await GetToken(token);
    }

    private async Task<ActionResult<TokenDto>> GetToken(Token token)
    {
      token.Issued = dateTimeProvider.Now;
      token.RefreshValue = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
      token.Value = GenerateToken(token.User);

      await Context.SaveChangesAsync();

      return Ok(Mapper.Map<TokenDto>(token));
    }

    private string GenerateToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenDescriptor = new SecurityTokenDescriptor()
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Id.ToString()),
        }),
        Expires = dateTimeProvider.Now.AddDays(7).UtcDateTime,
        IssuedAt = dateTimeProvider.Now.UtcDateTime,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(configuration.GetSigningKey()), SecurityAlgorithms.HmacSha512Signature),
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
