using System;
using System.Collections.Generic;
using System.Linq;

namespace EmpoweredPixels.Utilities
{
  public class BetterActivator
  {
    public static T CreateInstance<T>(params object[] args)
    {
      var constructors = typeof(T)
        .GetConstructors();

      var bestmatch = constructors
        .OrderByDescending(c => c
              .GetParameters()
              .Select(p => p.ParameterType)
              .Intersect(args.Select(o => o.GetType()))
              .Count())
        .FirstOrDefault();

      if (bestmatch == null)
      {
        throw new ArgumentNullException(nameof(bestmatch));
      }

      List<object> parameters = new List<object>();

      // Order objects by parameter order. Use null for missing objects
      foreach (Type type in bestmatch
        .GetParameters()
        .Select(p => p.ParameterType))
      {
        var obj = args
          .Where(o => type.IsAssignableFrom(o.GetType()) || o.GetType().IsAssignableFrom(type))
          .FirstOrDefault();
        parameters.Add(obj);
      }

      return (T)bestmatch.Invoke(parameters.ToArray());
    }
  }
}
