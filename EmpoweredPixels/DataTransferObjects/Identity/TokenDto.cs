namespace EmpoweredPixels.DataTransferObjects.Identity
{
  public class TokenDto
  {
    public long UserId { get; set; }

    public string Token { get; set; }

    public string Refresh { get; set; }
  }
}
