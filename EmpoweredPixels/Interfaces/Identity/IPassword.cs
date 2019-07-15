namespace EmpoweredPixels.Interfaces.Identity
{
  public interface IPassword
  {
    string Password { get; set; }

    string Salt { get; set; }
  }
}
