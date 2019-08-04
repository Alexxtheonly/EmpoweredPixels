using System;
using System.Linq.Expressions;

namespace EmpoweredPixels.Utilities.Paging.Extensions
{
  public static class NumberFilterExtension
  {
    public static Expression<Func<T, bool>> GetExpression<T>(this NumberFilter numberFilter)
      where T : class
    {
      ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "p");
      MemberExpression memberExpression = parameterExpression.GetPropertyExpression(numberFilter.PropertyName);

      Expression lowerboundExpression = null;
      Expression upperboundExpression = null;

      if (numberFilter.Lowerbound != null)
      {
        lowerboundExpression = Expression.GreaterThanOrEqual(memberExpression, Expression.Constant(Convert.ChangeType(numberFilter.Lowerbound.Value, memberExpression.Type)));
      }

      if (numberFilter.Upperbound != null)
      {
        upperboundExpression = Expression.LessThanOrEqual(memberExpression, Expression.Constant(Convert.ChangeType(numberFilter.Upperbound.Value, memberExpression.Type)));
      }

      // if no boundaries are set return null
      if (lowerboundExpression == null && upperboundExpression == null)
      {
        return null;
      }

      // if both boundaries are set, combine both expressions
      if (lowerboundExpression != null && upperboundExpression != null)
      {
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(lowerboundExpression, upperboundExpression), parameterExpression);
      }

      // only return the expression that is not null
      return Expression.Lambda<Func<T, bool>>(lowerboundExpression ?? upperboundExpression, parameterExpression);
    }
  }
}
