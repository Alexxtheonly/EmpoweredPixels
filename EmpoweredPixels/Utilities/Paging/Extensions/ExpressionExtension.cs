using System;
using System.Linq.Expressions;

namespace EmpoweredPixels.Utilities.Paging.Extensions
{
  public static class ExpressionExtension
  {
    public static MemberExpression GetPropertyExpression(this ParameterExpression parameterExpression, string propertyTree)
    {
      if (string.IsNullOrEmpty(propertyTree))
      {
        throw new ArgumentException(nameof(propertyTree));
      }

      Expression memberExpression = parameterExpression;

      Type previousType = parameterExpression.Type;
      foreach (var propertyNameData in propertyTree.Split('.'))
      {
        var propertyName = char.ToUpper(propertyNameData[0]).ToString() + propertyNameData.Substring(1);

        var property = previousType.GetProperty(propertyName);

        if (property == null)
        {
          throw new NullReferenceException($"{propertyName} does not exist");
        }

        previousType = property.PropertyType;

        memberExpression = Expression.Property(memberExpression, propertyName);
      }

      return (MemberExpression)memberExpression;
    }
  }
}
