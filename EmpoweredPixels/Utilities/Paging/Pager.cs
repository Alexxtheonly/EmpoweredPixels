using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmpoweredPixels.Utilities.Paging.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EmpoweredPixels.Utilities.Paging
{
  public class Pager
  {
    public static Page<T> GetPage<T>(IEnumerable<T> items, PagingOptions options)
      where T : class
    {
      return GetPage(items, options, i => i);
    }

    public static Page<TResult> GetPage<TSource, TResult>(IEnumerable<TSource> items, PagingOptions options, Expression<Func<TSource, TResult>> selector)
      where TSource : class
      where TResult : class
    {
      return new Pager()
        .GetPageInternal(items.AsQueryable(), options, selector);
    }

    public static async Task<Page<T>> GetPageAsync<T>(IQueryable<T> items, PagingOptions options)
      where T : class
    {
      return await GetPageAsync(items, options, i => i);
    }

    public static async Task<Page<TResult>> GetPageAsync<TSource, TResult>(IQueryable<TSource> items, PagingOptions options, Expression<Func<TSource, TResult>> selector)
      where TSource : class
      where TResult : class
    {
      return await new Pager()
        .GetPageInternalAsync(items, options, selector);
    }

    private async Task<Page<TResult>> GetPageInternalAsync<TSource, TResult>(IQueryable<TSource> items, PagingOptions options, Expression<Func<TSource, TResult>> selector)
      where TSource : class
      where TResult : class
    {
      var filtered = Filter(items, options);
      var sorted = Sort(filtered, options.SortSetting);

      var elements = await sorted
        .GetPage(options.PageNumber, options.PageSize)
        .Select(selector)
        .ToListAsync();

      return new Page<TResult>()
      {
        Items = elements,
        PageNumber = options.PageNumber,
        PageSize = options.PageSize,
        Total = await filtered.CountAsync()
      };
    }

    private Page<TResult> GetPageInternal<TSource, TResult>(IQueryable<TSource> items, PagingOptions options, Expression<Func<TSource, TResult>> selector)
      where TSource : class
      where TResult : class
    {
      var filtered = Filter(items, options);
      var sorted = Sort(filtered, options.SortSetting);

      var elements = sorted
        .GetPage(options.PageNumber, options.PageSize)
        .Select(selector)
        .ToList();

      return new Page<TResult>()
      {
        Items = elements,
        PageNumber = options.PageNumber,
        PageSize = options.PageSize,
        Total = filtered.Count(),
      };
    }

    private IQueryable<T> Filter<T>(IQueryable<T> items, PagingOptions paging)
      where T : class
    {
      var filteredItems = items;

      foreach (var stringFilter in paging.StringFilters)
      {
        Expression<Func<T, bool>> expression = stringFilter.GetExpression<T>();
        if (expression != null)
        {
          filteredItems = filteredItems.Where(expression);
        }
      }

      foreach (var numberFilter in paging.NumberFilters)
      {
        Expression<Func<T, bool>> expression = numberFilter.GetExpression<T>();
        if (expression != null)
        {
          filteredItems = filteredItems.Where(expression);
        }
      }

      return filteredItems;
    }

    private IQueryable<T> Sort<T>(IQueryable<T> items, SortSetting sortSetting)
      where T : class
    {
      if (sortSetting == null)
      {
        return items;
      }

      return items.OrderBy(sortSetting.PropertyName, !sortSetting.Descending);
    }
  }
}
