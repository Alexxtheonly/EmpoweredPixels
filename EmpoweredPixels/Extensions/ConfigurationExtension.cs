using System.Text;
using EmpoweredPixels.Utilities;
using Microsoft.Extensions.Configuration;

namespace EmpoweredPixels.Extensions
{
  public static class ConfigurationExtension
  {
    public static byte[] GetSigningKey(this IConfiguration configuration)
    {
      return Encoding.UTF8.GetBytes(configuration.GetValue<string>(Constants.SigningKey));
    }
  }
}
