using System;
using System.Security.Cryptography;
using EmpoweredPixels.Interfaces.Identity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EmpoweredPixels.Extensions
{
  public static class IPasswordExtension
  {
    public static void SetPassword(this IPassword entity, string password)
    {
      if (string.IsNullOrEmpty(entity.Salt))
      {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
          rng.GetBytes(salt);
        }

        entity.Salt = Convert.ToBase64String(salt);
      }

      entity.Password = password.ToPassword(entity.Salt);
    }

    public static bool IsValidPassword(this IPassword entity, string password)
    {
      if (password == entity.Password)
      {
        return true;
      }

      return string.Equals(entity.Password, (password ?? string.Empty).ToPassword(entity.Salt));
    }

    public static string ToPassword(this string value, string base64Salt)
    {
      return Convert.ToBase64String(KeyDerivation.Pbkdf2(
        password: value,
        salt: Convert.FromBase64String(base64Salt),
        prf: KeyDerivationPrf.HMACSHA512,
        iterationCount: 10000,
        numBytesRequested: 256 / 8));
    }
  }
}
