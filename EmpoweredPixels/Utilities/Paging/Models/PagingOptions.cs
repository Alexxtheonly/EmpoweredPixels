using System.Collections.Generic;

namespace EmpoweredPixels.Utilities.Paging
{
  public class PagingOptions
  {
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public ICollection<StringFilter> StringFilters { get; set; } = new List<StringFilter>();

    public ICollection<NumberFilter> NumberFilters { get; set; } = new List<NumberFilter>();

    public SortSetting SortSetting { get; set; }
  }
}
