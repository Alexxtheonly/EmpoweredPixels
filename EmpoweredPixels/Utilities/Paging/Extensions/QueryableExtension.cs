using System;
using System.Linq;
using System.Linq.Expressions;

namespace EmpoweredPixels.Utilities.Paging.Extensions
{
  public static class QueryableExtension
  {
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> items, string propertyName)
    {
      return items.OrderBy(propertyName, true);
    }

    public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> items, string propertyName)
    {
      return items.OrderBy(propertyName, false);
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> items, string propertyName, bool ascending)
    {
      ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
      MemberExpression memberExpression = parameterExpression.GetPropertyExpression(propertyName);

      Expression expression = memberExpression;

      LambdaExpression lambdaExpression = Expression.Lambda(expression, parameterExpression);

      string order = ascending ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);

      var callExpression = Expression.Call(typeof(Queryable), order, new Type[] { typeof(T), expression.Type }, items.Expression, Expression.Quote(lambdaExpression));

      return items.Provider.CreateQuery<T>(callExpression);
    }
  }
}
