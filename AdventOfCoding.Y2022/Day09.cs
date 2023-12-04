using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day09 : FileDay
  {
    private (int, int) Follow((int, int) head, (int, int) tail)
    {
      if (head == tail) return tail;
      if (Math.Abs(head.Item1 - tail.Item1) < 2 && Math.Abs(head.Item2 - tail.Item2) < 2) return tail;
      if (head.Item1 == tail.Item1) return (tail.Item1, tail.Item2 < head.Item2 ? tail.Item2 + 1 : tail.Item2 - 1);
      if (head.Item2 == tail.Item2) return (tail.Item1 < head.Item1 ? tail.Item1 + 1 : tail.Item1 - 1, tail.Item2);
      return (tail.Item1 < head.Item1 ? tail.Item1 + 1 : tail.Item1 - 1, tail.Item2 < head.Item2 ? tail.Item2 + 1 : tail.Item2 - 1);
    }

    public override string Task1()
    {
      var head = (0, 0);
      var tail = (0, 0);

      var visited = new HashSet<(int, int)>
      {
        tail
      };

      foreach (var line in ReadLines())
      {
        var dir = line[0];
        var dist = GetInt(line, 2);

        for (var i = 0; i < dist; i++)
        {
          head = dir switch
          {
            'L' => (head.Item1 - 1, head.Item2),
            'U' => (head.Item1, head.Item2 + 1),
            'R' => (head.Item1 + 1, head.Item2),
            'D' => (head.Item1, head.Item2 - 1),
            _ => throw new InvalidOperationException()
          };

          tail = Follow(head, tail);

          visited.Add(tail);
        }
      }

      return visited.Count.ToString();
    }

    public override string Task2()
    {
      var rope = new[] {
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
      };

      var visited = new HashSet<(int, int)>
      {
        rope[rope.Length - 1]
      };

      foreach (var line in ReadLines())
      {
        var dir = line[0];
        var dist = GetInt(line, 2);

        for (var i = 0; i < dist; i++)
        {
          rope[0] = dir switch
          {
            'L' => (rope[0].Item1 - 1, rope[0].Item2),
            'U' => (rope[0].Item1, rope[0].Item2 + 1),
            'R' => (rope[0].Item1 + 1, rope[0].Item2),
            'D' => (rope[0].Item1, rope[0].Item2 - 1),
            _ => throw new InvalidOperationException()
          };

          for (var r = 1; r < rope.Length; r++)
          {
            rope[r] = Follow(rope[r - 1], rope[r]);
          }

          visited.Add(rope[rope.Length - 1]);
        }
      }

      return visited.Count.ToString();
    }
  }
}
