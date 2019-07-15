using System;

namespace EmpoweredPixels.Models.Identity
{
  public class Token
  {
    public Guid Id { get; set; }

    public long UserId { get; set; }

    public string Value { get; set; }

    public string RefreshValue { get; set; }

    public DateTimeOffset Issued { get; set; }

    public virtual User User { get; set; }

    public bool Equals(Token other)
    {
      if (other == null)
      {
        return false;
      }

      if (Id != other.Id)
      {
        return false;
      }

      if (UserId != other.UserId)
      {
        return false;
      }

      if (!string.Equals(Value, other.Value))
      {
        return false;
      }

      if (!string.Equals(RefreshValue, other.RefreshValue))
      {
        return false;
      }

      if (Issued != other.Issued)
      {
        return false;
      }

      return true;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as Token);
    }

    public override int GetHashCode()
    {
      return new { Id, UserId, Value, RefreshValue, Issued }.GetHashCode();
    }

    public override string ToString()
    {
      return $"{nameof(Id)}:{Id} {nameof(Token)}:{Value} {nameof(Issued)}:{Issued}";
    }
  }
}
