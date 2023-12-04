using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Extensions
{
  public static class IEnumerableExtensions
  {
    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, TSource element) where TSource : IEquatable<TSource>
    {
      foreach (var item in source)
      {
        if (item.Equals(element)) continue;

        yield return item;
      }
    }

  }
}
