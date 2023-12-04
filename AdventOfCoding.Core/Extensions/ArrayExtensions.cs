using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Extensions
{
  public static class ArrayExtensions
  {
    public static bool Contains<T>(this T[] array, T value, int from, int length) where T : IEquatable<T>
    {
      var limit = Math.Min(array.Length, from + length);

      for (var i = from; i < limit; i++)
      {
        if (array[i]?.Equals(value) ?? value == null) return true;
      }

      return false;
    }

    public static T[] Append<T>(this T[] orig, T value)
    {
      var result = new T[orig.Length + 1];
      orig.CopyTo(result, 0);
      result[orig.Length] = value;
      return result;
    }

    public static T[] Prepend<T>(this T[] orig, T value)
    {
      var result = new T[orig.Length + 1];
      orig.CopyTo(result, 1);
      result[0] = value;
      return result;
    }

    public static T[] Copy<T>(this T[] orig)
    {
      var result = new T[orig.Length];
      orig.CopyTo(result, 0);
      return result;
    }

    public static bool IsSame<T>(this T[] a, T[] b) where T : struct, IEquatable<T>
    {
      if (a.Length != b.Length) return false;

      for (var i = 0; i < a.Length; i++)
      {
        if (!a[i].Equals(b[i])) return false;
      }

      return true;
    }

  }
}
