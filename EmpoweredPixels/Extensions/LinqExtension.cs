using System;
using System.Collections.Generic;
using System.Linq;

namespace EmpoweredPixels.Extensions
{
  public static class LinqExtension
  {
    public static T Random<T>(this IEnumerable<T> items, Random random)
    {
      if (!items.Any())
      {
        throw new Exception("items may not be empty");
      }

      return items.ElementAt(random.Next(0, items.Count()));
    }

    public static IEnumerable<List<T>> Split<T>(this List<T> source, int count)
    {
      int rangeSize = source.Count / count;
      int additionalItems = source.Count % count;
      int index = 0;

      while (index < source.Count)
      {
        int currentRangeSize = rangeSize + ((additionalItems > 0) ? 1 : 0);
        yield return source.GetRange(index, currentRangeSize);
        index += currentRangeSize;
        additionalItems--;
      }
    }
  }
}
