using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day15 : FileDay
  {
    private static readonly Regex _regex = new(@"(?<name>\w+): capacity (?<capacity>-?\d+), durability (?<durability>-?\d+), flavor (?<flavor>-?\d+), texture (?<texture>-?\d+), calories (?<calories>-?\d+)", RegexOptions.Compiled);

    // This is not robust enough, works for only 4 ingredients
    private IEnumerable<int[]> GetQuantities()
    {
      for (var i0 = 0; i0 <= 100; i0++)
      {
        for (var i1 = 0; i1 <= 100 - i0; i1++)
        {
          for (var i2 = 0; i2 <= 100 - i1 - i0; i2++)
          {
            yield return new[] { i0, i1, i2, 100 - i0 - i1 - i2 };
          }
        }
      }
    }

    public override string Task1()
    {
      var ingredients = new List<(int capacity, int durability, int flavor, int texture, int calories)>();
      var max = 0;

      foreach (var line in ReadLines())
      {
        var match = _regex.Match(line);

        ingredients.Add((GetInt(match.Groups[2].Value), GetInt(match.Groups[3].Value), GetInt(match.Groups[4].Value), GetInt(match.Groups[5].Value), GetInt(match.Groups[6].Value)));
      }

      foreach (var q in GetQuantities())
      {
        var c = 0;
        var d = 0;
        var f = 0;
        var t = 0;

        for (var i = 0; i < q.Length; i++)
        {
          c += q[i] * ingredients[i].capacity;
          d += q[i] * ingredients[i].durability;
          f += q[i] * ingredients[i].flavor;
          t += q[i] * ingredients[i].texture;
        }

        var current = Math.Max(0, c) * Math.Max(0, d) * Math.Max(0, f) * Math.Max(0, t);

        if ( max < current)
        {
          max = current;
        }
      }

      return max.ToString();
    }

    public override string Task2()
    {
      var ingredients = new List<(int capacity, int durability, int flavor, int texture, int calories)>();
      var max = 0;

      foreach (var line in ReadLines())
      {
        var match = _regex.Match(line);

        ingredients.Add((GetInt(match.Groups[2].Value), GetInt(match.Groups[3].Value), GetInt(match.Groups[4].Value), GetInt(match.Groups[5].Value), GetInt(match.Groups[6].Value)));
      }

      foreach (var q in GetQuantities())
      {
        var c = 0;
        var d = 0;
        var f = 0;
        var t = 0;
        var cal = 0;

        for (var i = 0; i < q.Length; i++)
        {
          c += q[i] * ingredients[i].capacity;
          d += q[i] * ingredients[i].durability;
          f += q[i] * ingredients[i].flavor;
          t += q[i] * ingredients[i].texture;
          cal += q[i] * ingredients[i].calories;
        }

        var current = Math.Max(0, c) * Math.Max(0, d) * Math.Max(0, f) * Math.Max(0, t);

        if (cal == 500 && max < current)
        {
          max = current;
        }
      }

      return max.ToString();
    }
  }
}
