using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2015
{
  internal class Day14 : FileDay
  {
    private static readonly Regex _regex = new Regex(@"(?<name>\w+) can fly (?<speed>\d+) km/s for (?<move>\d+) seconds, but then must rest for (?<rest>\d+) seconds\.", RegexOptions.Compiled);
    private const int target = 2503;

    public override string Task1()
    {
      var max = 0;

      foreach (var line in ReadLines())
      {
        var match = _regex.Match(line);

        var speed = GetInt(match.Groups[2].Value);
        var move = GetInt(match.Groups[3].Value);
        var rest = GetInt(match.Groups[4].Value);

        var iteration = move + rest;

        var distance = (target / iteration) * (speed * move) + Math.Min(move, (target % iteration)) * speed;

        if (distance > max)
        {
          max = distance;
        }
      }

      return max.ToString();
    }

    public override string Task2()
    {
      var stats = new List<(int speed, int move, int rest, int iteration)>();

      foreach (var line in ReadLines())
      {
        var match = _regex.Match(line);

        var speed = GetInt(match.Groups[2].Value);
        var move = GetInt(match.Groups[3].Value);
        var rest = GetInt(match.Groups[4].Value);

        stats.Add((speed, move, rest, move + rest));
      }

      var distances = new int[stats.Count];
      var points = new int[stats.Count];

      for (var time = 0; time < target; time++)
      {
        var max = 0;

        for (var deer = 0; deer < stats.Count; deer++)
        {
          var distance = distances[deer];

          if ((time % stats[deer].iteration) < stats[deer].move)
          {
            distance += stats[deer].speed;

            distances[deer] = distance;
          }

          if (distance > max)
          {
            max = distance;
          }
        }

        for (var deer = 0; deer < stats.Count; deer++)
        {
          if (distances[deer] == max)
          {
            points[deer]++;
          }
        }
      }

      return points.Max().ToString();
    }
  }
}
