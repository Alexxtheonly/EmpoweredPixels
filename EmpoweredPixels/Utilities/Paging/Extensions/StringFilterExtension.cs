using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EmpoweredPixels.Utilities.Paging.Extensions
{
  public static class StringFilterExtension
  {
    private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
    private static readonly MethodInfo StringToLowerMethod = typeof(string).GetMethod(nameof(string.ToLower), new Type[] { });

    public static Expression<Func<T, bool>> GetExpression<T>(this StringFilter filter)
      where T : class
    {
      ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
      MemberExpression memberExpression = parameterExpression.GetPropertyExpression(filter.PropertyName);

      var stringFilterExpression = memberExpression;

      Expression toLowerExpression = Expression.Call(stringFilterExpression, StringToLowerMethod);

      return Expression.Lambda<Func<T, bool>>(Expression.Call(toLowerExpression, StringContainsMethod, Expression.Constant(filter.Query.ToLowerInvariant())), parameterExpression);
    }
  }
}
