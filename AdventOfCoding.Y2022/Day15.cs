using AdventOfCoding.Core;
using AdventOfCoding.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day15 : FileDay
  {
    private static readonly Regex _inputRegex = new Regex(@"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)$");

    private int GetSensorRange(int line, ((int x, int y) sensor, (int x, int y) beacon, int distance) pair)
    {
      if ((pair.sensor.y - pair.distance > line && pair.sensor.y + pair.distance < line)) return 0;

      return pair.distance - Math.Abs(pair.sensor.y - line);
    }

    private List<((int x, int y) sensor, (int x, int y) beacon, int distance)> GetSensors(out int minX, out int maxX)
    {
      var list = new List<((int x, int y) sensor, (int x, int y) beacon, int distance)>();
      minX = int.MaxValue;
      maxX = int.MinValue;
      foreach (var line in ReadLines())
      {
        var match = _inputRegex.Match(line);

        var x1 = GetInt(match.Groups[1].Value);
        var y1 = GetInt(match.Groups[2].Value);
        var x2 = GetInt(match.Groups[3].Value);
        var y2 = GetInt(match.Groups[4].Value);

        var p1 = (x1, y1);
        var p2 = (x2, y2);

        var dist = Math.Abs(p1.x1 - p2.x2) + Math.Abs(p1.y1 - p2.y2);

        if (minX > p1.x1 - dist)
        {
          minX = p1.x1 - dist;
        }
        if (maxX < p1.x1 + dist)
        {
          maxX = p1.x1 + dist;
        }

        list.Add((p1, p2, dist));
      }

      return list;
    }

    public override string Task1()
    {
      int minX, maxX;
      var list = GetSensors(out minX, out maxX);

      const int row = 2000000;

      var isnot = 0;

      List<(int f, int t)> ranges = GetRanges(list, row);

      for (var i = minX; i <= maxX; i++)
      {
        if (ranges.Any(a => a.f <= i && a.t >= i))
        {
          isnot++;
        }
      }

      return isnot.ToString();
    }

    private List<(int f, int t)> GetRanges(List<((int x, int y) sensor, (int x, int y) beacon, int distance)> list, int row, bool skipBeacons = true)
    {
      var ranges = new List<(int f, int t)>();

      foreach (var pair in list)
      {
        var range = GetSensorRange(row, pair);

        if (range > 0)
        {
          if (skipBeacons && pair.beacon.y == row)
          {
            if (pair.beacon.x < pair.sensor.x)
            {
              ranges.Add((pair.sensor.x - range + 1, pair.sensor.x + range));
            }
            else
            {
              ranges.Add((pair.sensor.x - range, pair.sensor.x + range - 1));
            }
          }
          else
          {
            ranges.Add((pair.sensor.x - range, pair.sensor.x + range));
          }
        }
      }

      return ranges;
    }

    public override string Task2()
    {
      //const int limit = 20;
      const int limit = 4000000;

      string getResult(int x, int y)
      {
        var big = new BigInteger(x);
        big *= limit;
        big += y;

        return big.ToString();
      }

      var list = GetSensors(out _, out _);

      for (var y = 0; y < limit; y++)
      {
        var ranges = GetRanges(list, y, false);
        ranges.Sort((a, b) => a.f.CompareTo(b.f));

        if (ranges.Count == 0) return y.ToString();

        var (minX, maxX) = ranges[0];

        if (minX > 0) return y.ToString();

        for (var i = 1; i < ranges.Count; i++)
        {
          var range = ranges[i];
          if (maxX >= range.f)
          {
            if (maxX < range.t)
            {
              maxX = range.t;
            }
          }
          else
          {
            return getResult(maxX + 1, y);
          }
        }

        if (maxX < limit) return getResult(maxX + 1, y);
      }

      return null;
    }
  }
}