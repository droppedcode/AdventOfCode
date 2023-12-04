using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Extensions
{
  /// <summary>
  /// Extension methods for <see cref="List{T}"/>
  /// </summary>
  public static class ListExtensions
  {
    /// <summary>
    /// Get element index or add to the list.
    /// </summary>
    /// <typeparam name="T">Type of the element.</typeparam>
    /// <param name="list">List.</param>
    /// <param name="value">Value to get index for.</param>
    /// <returns>The index of the element.</returns>
    public static int GetIndexOrAdd<T>(this List<T> list, T value) where T : IComparable<T>
    {
      for (var i = 0; i < list.Count; i++)
      {
        if (list[i].Equals(value)) return i;
      }

      list.Add(value);
      return list.Count - 1;
    }

    public static bool Contains<T>(this List<T> list, T value, int from, int count) where T : IEquatable<T>
    {
      var limit = Math.Min(list.Count, from + count);

      for (var i = from; i < limit; i++)
      {
        if (list[i]?.Equals(value) ?? value == null) return true;
      }

      return false;
    }

    public static void EnsureCount<T>(this List<T> list, int count, Func<T> factory)
    {
      while (list.Count < count)
      {
        list.Add(factory());
      }
    }

  }
}