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
  internal class Day09 : FileDay
  {
    public override string Task1()
    {
      var locations = new List<string>();
      var routes = new Dictionary<(int, int), int>();

      foreach (var line in ReadLines())
      {
        var a = new string(GetWord(line));
        var b = new string(GetWord(line, a.Length + 4));
        var dist = GetInt(line, a.Length + b.Length + 7);

        var ai = locations.GetIndexOrAdd(a);
        var bi = locations.GetIndexOrAdd(b);

        routes.Add((ai, bi), dist);
        routes.Add((bi, ai), dist);
      }

      var min = int.MaxValue;

      foreach (var permutation in Combinatorics.GetPermutations(Enumerable.Range(0, locations.Count)))
      {
        var current = permutation.First();
        var distance = 0;

        foreach (var location in permutation.Skip(1))
        {
          if (!routes.TryGetValue((current, location), out var dist))
          {
            distance = int.MaxValue;
            break;
          }
          else
          {
            distance += dist;
            current = location;
          }
        }

        if (distance < min)
        {
          min = distance;
        }
      }

      return min.ToString();
    }

    public override string Task2()
    {
      var locations = new List<string>();
      var routes = new Dictionary<(int, int), int>();

      foreach (var line in ReadLines())
      {
        var a = new string(GetWord(line));
        var b = new string(GetWord(line, a.Length + 4));
        var dist = GetInt(line, a.Length + b.Length + 7);

        var ai = locations.GetIndexOrAdd(a);
        var bi = locations.GetIndexOrAdd(b);

        routes.Add((ai, bi), dist);
        routes.Add((bi, ai), dist);
      }

      var max = 0;

      foreach (var permutation in Combinatorics.GetPermutations(Enumerable.Range(0, locations.Count)))
      {
        var current = permutation.First();
        var distance = 0;

        foreach (var location in permutation.Skip(1))
        {
          if (!routes.TryGetValue((current, location), out var dist))
          {
            distance = int.MaxValue;
            break;
          }
          else
          {
            distance += dist;
            current = location;
          }
        }

        if (distance > max)
        {
          max = distance;
        }
      }

      return max.ToString();
    }
  }
}
