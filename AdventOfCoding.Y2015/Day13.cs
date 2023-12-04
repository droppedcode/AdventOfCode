using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using AdventOfCoding.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day13 : FileDay
  {
    public override string Task1()
    {
      var names = new List<string>();

      var deltas = new Dictionary<(int, int), int>();

      foreach (var line in ReadLines())
      {
        var a = new string(GetWord(line));
        var op = GetWord(line, a.Length + 7);
        var delta = GetInt(line, out var index, a.Length + 12);
        var b = new string(GetWord(line, index + 35, '.'));

        var ai = names.GetIndexOrAdd(a);
        var bi = names.GetIndexOrAdd(b);

        deltas[(ai, bi)] = op[0] == 'g' ? delta : -delta;
      }

      var max = int.MinValue;

      foreach (var permutation in Combinatorics.GetPermutations(Enumerable.Range(0, names.Count)))
      {
        var first = permutation.First();
        var prev = first;

        var sum = 0;

        int delta;

        foreach (var p in permutation.Skip(1))
        {
          if (deltas.TryGetValue((prev, p), out delta))
          {
            sum += delta;
          }

          if (deltas.TryGetValue((p, prev), out delta))
          {
            sum += delta;
          }

          prev = p;
        }

        if (deltas.TryGetValue((prev, first), out delta))
        {
          sum += delta;
        }

        if (deltas.TryGetValue((first, prev), out delta))
        {
          sum += delta;
        }

        if (sum > max)
        {
          max = sum;
        }
      }

      return max.ToString();
    }

    public override string Task2()
    {
      var names = new List<string>();

      var deltas = new Dictionary<(int, int), int>();

      foreach (var line in ReadLines())
      {
        var a = new string(GetWord(line));
        var op = GetWord(line, a.Length + 7);
        var delta = GetInt(line, out var index, a.Length + 12);
        var b = new string(GetWord(line, index + 35, '.'));

        var ai = names.GetIndexOrAdd(a);
        var bi = names.GetIndexOrAdd(b);

        deltas[(ai, bi)] = op[0] == 'g' ? delta : -delta;
      }

      var max = int.MinValue;

      foreach (var permutation in Combinatorics.GetPermutations(Enumerable.Range(0, names.Count + 1)))
      {
        var first = permutation.First();
        var prev = first;

        var sum = 0;

        int delta;

        foreach (var p in permutation.Skip(1))
        {
          if (deltas.TryGetValue((prev, p), out delta))
          {
            sum += delta;
          }

          if (deltas.TryGetValue((p, prev), out delta))
          {
            sum += delta;
          }

          prev = p;
        }

        if (deltas.TryGetValue((prev, first), out delta))
        {
          sum += delta;
        }

        if (deltas.TryGetValue((first, prev), out delta))
        {
          sum += delta;
        }

        if (sum > max)
        {
          max = sum;
        }
      }

      return max.ToString();
    }
  }
}
