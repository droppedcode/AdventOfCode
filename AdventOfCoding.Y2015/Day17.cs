using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day17 : FileDay
  {
    protected static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> values, int length)
    {
      if (length == 1)
      {
        yield return Array.Empty<T>();
        yield return values;
      }
      else
      {
        var value = values.First();
        foreach (var next in GetCombinations(values.Skip(1), length - 1))
        {
          yield return next;
          yield return next.Prepend(value);
        }
      }
    }

    public override string Task1()
    {
      var packages = new List<(int index, int size)>();
      var index = 0;

      foreach (var line in ReadLines())
      {
        packages.Add((index, GetInt(line)));
        index++;
      }

      var count = 0;

      foreach (var p in GetCombinations(packages, packages.Count))
      {
        var sum = 0;

        foreach (var i in p)
        {
          sum += i.size;
        }

        if (sum == 150)
        {
          count++;
        }
      }

      return count.ToString();
    }

    public override string Task2()
    {
      var packages = new List<(int index, int size)>();
      var index = 0;

      foreach (var line in ReadLines())
      {
        packages.Add((index, GetInt(line)));
        index++;
      }

      var count = 0;
      var min = int.MaxValue;

      foreach (var p in GetCombinations(packages, packages.Count))
      {
        var sum = 0;
        var containers = 0;

        foreach (var i in p)
        {
          sum += i.size;
          containers++;
        }

        if (sum == 150)
        {
          if (containers == min)
          {
            count++;
          }
          else if (containers < min)
          {
            min = containers;
            count = 1;
          }
        }
      }

      return count.ToString();
    }
  }
}
