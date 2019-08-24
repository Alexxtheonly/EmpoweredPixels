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
  }
}
