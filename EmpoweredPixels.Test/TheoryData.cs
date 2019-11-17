using System.Collections;
using System.Collections.Generic;

namespace EmpoweredPixels.Test
{
  public abstract class TheoryData : IEnumerable<object[]>
  {
    private readonly List<object[]> data = new List<object[]>();

    public IEnumerator<object[]> GetEnumerator()
    {
      return data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    protected void AddRow(params object[] values)
    {
      data.Add(values);
    }
  }

}
