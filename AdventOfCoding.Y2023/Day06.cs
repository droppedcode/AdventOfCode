using AdventOfCoding.Core;
using AdventOfCoding.Core.Extensions;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2023
{
  internal class Day06 : FileDay
  {
    public override string Task1()
    {
      var times = new List<int>();
      foreach (var text in ReadLines().First().AsSpan(11).SplitBy(SearchValues.Create([' '])))
      {
        if (text.Value.Length > 0)
        {
          times.Add(text.Value.ParseFirstInt());
        }
      }
      var distances = new List<int>();
      foreach (var text in ReadLines().Skip(1).First().AsSpan(11).SplitBy(SearchValues.Create([' '])))
      {
        if (text.Value.Length > 0)
        {
          distances.Add(text.Value.ParseFirstInt());
        }
      }

      var sum = 1;

      for (var i = 0; i < times.Count; i++)
      {
        var time = times[i];
        var distance = distances[i];

        var perm = 0;

        for (var t = 0; t < time; t++)
        {
          if ((time - t) * t > distance)
          {
            perm++;
          }
        }

        sum *= perm;
      }

      return sum.ToString();
    }

    // Flaky solution to try find the bounds of the range
    /*
    public override string Task2()
    {
      var times = new List<ulong>();
      foreach (var text in ReadLines().First().Replace(" ", "").AsSpan(5).SplitBy(SearchValues.Create([' '])))
      {
        if (text.Value.Length > 0)
        {
          times.Add(text.Value.ParseFirstULong());
        }
      }
      var distances = new List<ulong>();
      foreach (var text in ReadLines().Skip(1).First().Replace(" ", "").AsSpan(9).SplitBy(SearchValues.Create([' '])))
      {
        if (text.Value.Length > 0)
        {
          distances.Add(text.Value.ParseFirstULong());
        }
      }

      return Count(times[0], distances[0]).ToString();
    }

    private ulong Count(ulong time, ulong distance)
    {
      ulong step = time / 16;

      var min = FindMin(distance, time, 0ul, step, time);
      var max = FindMax(distance, time, time, step);

      return max - min - min;
    }

    private ulong FindMin(ulong distance, ulong time, ulong start, ulong step, ulong max)
    {
      if (step == 0) return start + 1;

      for (var t = start; t <= max; t += step)
      {
        if (IsMore(distance, time, t))
        {
          return FindMin(distance, time, t - step, step / 2, t);
        }
      }

      return time + step;
    }

    private static bool IsMore(ulong distance, ulong time, ulong t)
    {
      try
      {
        checked
        {
          return (time - t) * t > distance;
        }
      }
      catch (OverflowException) {
        return true;
      }
    }

    private ulong FindMax(ulong distance, ulong time, ulong start, ulong step)
    {
      if (step == 0) return start + 1;
    
      for (var t = start; t <= time; t -= step)
      {
        if (IsMore(distance, time, t))
        {
          return FindMax(distance, time, t + step, step / 2);
        }
      }

      return time;
    }
    */

    /// Brute force solution
    public override string Task2()
    {
      var times = new List<ulong>();
      foreach (var text in ReadLines().First().Replace(" ", "").AsSpan(5).SplitBy(SearchValues.Create([' '])))
      {
        if (text.Value.Length > 0)
        {
          times.Add(text.Value.ParseFirstULong());
        }
      }
      var distances = new List<ulong>();
      foreach (var text in ReadLines().Skip(1).First().Replace(" ", "").AsSpan(9).SplitBy(SearchValues.Create([' '])))
      {
        if (text.Value.Length > 0)
        {
          distances.Add(text.Value.ParseFirstULong());
        }
      }

      var sum = 1ul;

      for (var i = 0; i < times.Count; i++)
      {
        var time = times[i];
        var distance = distances[i];

        var perm = 0ul;

        for (var t = 0ul; t < time; t++)
        {
          if ((time - t) * t > distance)
          {
            perm++;
          }
        }

        sum *= perm;
      }

      return sum.ToString();
    }
  }
}
