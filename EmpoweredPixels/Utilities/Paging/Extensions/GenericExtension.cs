using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmpoweredPixels.Utilities.Paging.Extensions
{
  public static class GenericExtension
  {
    public static IQueryable<T> GetPage<T>(this IQueryable<T> items, int pagenumber, int elementCount)
    {
      return items
        .Skip(elementCount * (pagenumber - 1))
        .Take(elementCount);
    }

    public static async Task<Page<T>> GetPage<T>(this IQueryable<T> items, PagingOptions options)
      where T : class
    {
      return await Pager
        .GetPageAsync(items, options);
    }

    public static Page<T> GetPage<T>(this IEnumerable<T> items, PagingOptions options)
      where T : class
    {
      return items
        .GetPage(options, i => i);
    }

    public static async Task<Page<TResult>> GetPage<TSource, TResult>(this IQueryable<TSource> items, PagingOptions options, Expression<Func<TSource, TResult>> selector)
      where TSource : class
      where TResult : class
    {
      return await Pager.GetPageAsync(items, options, selector);
    }

    public static Page<TResult> GetPage<TSource, TResult>(this IEnumerable<TSource> items, PagingOptions options, Expression<Func<TSource, TResult>> selector)
      where TSource : class
      where TResult : class
    {
      return Pager
        .GetPage(items, options, selector);
    }
  }
}
