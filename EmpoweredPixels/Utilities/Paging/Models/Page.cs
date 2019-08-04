using System.Collections.Generic;

namespace EmpoweredPixels.Utilities.Paging
{
  public class Page<T>
  {
    public IEnumerable<T> Items { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int Total { get; set; }

    public bool Equals(Page<T> other)
    {
      if (other == null)
      {
        return false;
      }

      if (PageNumber != other.PageNumber)
      {
        return false;
      }

      if (PageSize != other.PageSize)
      {
        return false;
      }

      if (Total != other.Total)
      {
        return false;
      }

      return true;
    }

    public override bool Equals(object obj)
    {
      return Equals(obj as Page<T>);
    }

    public override int GetHashCode()
    {
      return new { PageNumber, PageSize, Total }.GetHashCode();
    }
  }
}
