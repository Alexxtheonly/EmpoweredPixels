using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EmpoweredPixels.Extensions
{
  public static class ClaimExtension
  {
    public static long? GetUserId(this IEnumerable<Claim> claims)
    {
      return claims
        .SingleOrDefault(o => o.Type == ClaimTypes.Name)
        ?.Value
        .AsLong();
    }
  }
}
