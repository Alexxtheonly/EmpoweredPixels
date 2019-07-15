using System;
using EmpoweredPixels.Interfaces.Identity;

namespace EmpoweredPixels.Models.Identity
{
  public class User : IPassword
  {
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public bool IsVerified { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset LastLogin { get; set; }

    public DateTimeOffset? Banned { get; set; }

    public bool Equals(User other)
    {
      if (other == null)
      {
        return false;
      }

      if (Id != other.Id)
      {
        return false;
      }

      if (!string.Equals(Name, other.Name))
      {
        return false;
      }

      if (!string.Equals(Email, other.Email))
      {
        return false;
      }

      if (!string.Equals(Password, other.Password))
      {
        return false;
      }

      if (!string.Equals(Salt, other.Salt))
      {
        return false;
      }

      if (Created != other.Created)
      {
        return false;
      }

      if (Banned != other.Banned)
      {
        return false;
      }

      return true;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as User);
    }

    public override int GetHashCode()
    {
      return new { Id, Name, Email, Password, Salt, Created, Banned }.GetHashCode();
    }

    public override string ToString()
    {
      return $"{nameof(Name)}:{Name}";
    }
  }
}
