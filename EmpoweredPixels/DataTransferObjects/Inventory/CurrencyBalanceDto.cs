using System;

namespace EmpoweredPixels.DataTransferObjects.Inventory
{
  public class CurrencyBalanceDto
  {
    public int Balance { get; set; }

    public Guid ItemId { get; set; }
  }
}
