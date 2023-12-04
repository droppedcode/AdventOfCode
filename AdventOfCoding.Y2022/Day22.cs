using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day22 : FileDay
  {
    private (byte[,] map, List<(Orientation, int)> path) ReadMap()
    {
      var lines = ReadLines().ToList();
      var map = new byte[lines.Take(lines.Count - 2).Max(m => m.Length), lines.Count - 2];

      for (var y = 0; y < lines.Count - 2; y++)
      {
        for (var x = 0; x < lines[y].Length; x++)
        {
          map[x, y] = lines[y][x] switch
          {
            '.' => 1,
            '#' => 2,
            _ => 0,
          };
        }
      }

      var path = new List<(Orientation, int)>();

      var orientation = Orientation.Up;
      var steps = 0;

      foreach (var c in "R" + lines[^1])
      {
        switch (c)
        {
          case 'R':
            orientation = (Orientation)(((int)orientation + 1) % 4);
            if (steps != 0)
            {
              path.Add((orientation, steps));
              steps = 0;
            }
            break;
          case 'L':
            orientation = (Orientation)(((int)orientation + 3) % 4);
            if (steps != 0)
            {
              path.Add((orientation, steps));
              steps = 0;
            }
            break;
          default:
            steps *= 10;
            steps += c - '0';
            break;
        }
      }

      return (map, path);
    }

    private (int x, int y) Move(byte[,] map, Orientation orientation, int steps, (int x, int y) pos)
    {
      switch (orientation)
      {
        case Orientation.Right:
          for (var i = 0; i < steps; i++)
          {
            if (map[pos.x + i + 1, pos.y] == 2)
            {
              return (pos.x + i, pos.y);
            }
            else if (map[pos.x + i + 1, pos.y] == 0)
            {
              for (var x = 0; x < map.GetLength(0); x++)
              {
                switch (map[x, pos.y])
                {
                  case 1:
                    pos = (x - i, pos.y);
                    break;
                  case 2:
                    return (pos.x + i, pos.y);
                }
              }
            }
          }
          return (pos.x + steps, pos.y);
        case Orientation.Down:
          break;
        case Orientation.Left:
          break;
        case Orientation.Up:
          break;
      }
    }

    public override string Task1()
    {
      var (map, path) = ReadMap();

      (int x, int y) pos = (0, 0);

      for (var x = 0; x < map.GetLength(0); x++)
      {
        if (map[x, 0] == 1)
        {
          pos = (x, 0);
        }
      }

      foreach (var (orientation, steps) in path)
      {
        pos = Move(map, orientation, steps, pos);
      }

      return (pos.y * 1000 + pos.x * 4 + )
    }

    public override string Task2()
    {
      throw new NotImplementedException();
    }

    private enum Orientation
    {
      Right,
      Down,
      Left,
      Up,
    }
  }
}
