using AdventOfCoding.Core.Buffers;
using AdventOfCoding.Core.Extensions;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Core.Helpers
{
  public static class Combinatorics
  {
    /// <summary>
    /// Get all permutations of the values.
    /// </summary>
    /// <param name="values">Values.</param>
    /// <param name="length">Length of the permutation, by default it should be the length of values.</param>
    /// <returns>The iterable permutations.</returns>
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> values) where T : IEquatable<T>
    {
      var list = values.ToArray();

      foreach (var permutation in Permute(list))
      {
        yield return permutation.ToArray();
      }
    }

    /// <summary>
    /// Permute an array.
    /// </summary>
    /// <param name="values">Values, the array to permute.</param>
    /// <returns>The iterable permutations, each iteration will modify the original array.</returns>
    public static IEnumerable<T[]> Permute<T>(T[] values)
    {
      var list = values.ToArray();

      return Permute(list, 0, list.Length - 1);
    }

    private static IEnumerable<T[]> Permute<T>(T[] values, int start, int end)
    {
      if (start == end)
      {
        yield return values;
      }
      else
      {
        for (var i = start; i <= end; i++)
        {
          Swap(ref values[start], ref values[i]);
          foreach (var result in Permute(values, start + 1, end))
          {
            yield return result;
          }
          Swap(ref values[start], ref values[i]);
        }
      }
    }

    private static void Swap<T>(ref T a, ref T b)
    {
      (b, a) = (a, b);
    }

    public static IEnumerable<T[]> GetCombinations<T>(T[] values, int count)
    {
      if (count > values.Length) throw new InvalidOperationException("The source array contains less items than the target array.");

      var result = new T[count];

      return GetCombinations(values, result, 0, 0);
    }

    private static IEnumerable<T[]> GetCombinations<T>(T[] values, T[] result, int valueIndex, int resultIndex)
    {
      if (resultIndex == result.Length)
      {
        yield return result;
      }
      else if (valueIndex == values.Length)
      {
        yield break;
      }
      else
      {
        foreach (var c in GetCombinations(values, result, valueIndex + 1, resultIndex))
        {
          yield return c;
        }

        result[resultIndex] = values[valueIndex];
        foreach (var c in GetCombinations(values, result, valueIndex + 1, resultIndex + 1))
        {
          yield return c;
        }
      }
    }
  }
}
