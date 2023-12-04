using AdventOfCoding.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCoding.Y2022
{
  internal class Day14 : FileDay
  {
    private List<((int x, int y) f, (int x, int y) t)> GetLines()
    {
      var lines = new List<((int, int), (int, int))>();

      foreach (var line in ReadLines())
      {
        var i = 0;
        var p0 = (GetInt(line, out i, i, ','), GetInt(line, out i, i, ' '));

        i += 3;

        for (; i < line.Length; i += 3)
        {
          var p1 = (GetInt(line, out i, i, ','), GetInt(line, out i, i, ' '));

          lines.Add((p0, p1));
          p0 = p1;
        }
      }

      return lines;
    }

    private (bool[,] map, (int x, int y) start) GetMap(bool hasFloor = false)
    {
      var lines = GetLines();

      var minX = int.MaxValue;
      var maxX = int.MinValue;
      var minY = int.MaxValue;
      var maxY = int.MinValue;

      void minMax((int x, int y) point)
      {
        if (point.x < minX)
        {
          minX = point.x;
        }
        if (point.x > maxX)
        {
          maxX = point.x;
        }
        if (point.y < minY)
        {
          minY = point.y;
        }
        if (point.y > maxY)
        {
          maxY = point.y;
        }
      }

      foreach (var line in lines)
      {
        minMax(line.f);
        minMax(line.t);
      }

      minMax((500, 0));

      // Add one more to both side for dropping
      minX--;
      maxX++;

      if (hasFloor)
      {
        maxY += 2;
        minX -= maxY - minY + 1;
        maxX += maxY - minY + 1;
      }

      var map = new bool[maxX - minX + 1, maxY - minY + 1];

      foreach (var line in lines)
      {
        var fx = Math.Min(line.f.x, line.t.x);
        var tx = Math.Max(line.f.x, line.t.x);
        var fy = Math.Min(line.f.y, line.t.y);
        var ty = Math.Max(line.f.y, line.t.y);

        for (var x = fx; x <= tx; x++)
        {
          for (var y = fy; y <= ty; y++)
          {
            map[x - minX, y - minY] = true;
          }
        }
      }

      if (hasFloor)
      {
        for (var x = 0; x < map.GetLength(0); x++)
        {
          map[x, map.GetLength(1) - 1] = true;
        }
      }

      return (map, (500 - minX, 0 - minY));
    }

    private bool DropSand(bool[,] map, (int x, int y) start)
    {
      var (x, y) = start;

      var my = map.GetLength(1);

      if (map[x, y]) return false;

      while (true)
      {
        // Console.WriteLine(DrawMap(map, start, (x, y)));

        if (y == my) return false;

        if (map[x, y])
        {
          if (!map[x - 1, y])
          {
            x--;
          }
          else if (!map[x + 1, y])
          {
            x++;
          }
          else
          {
            if (y == 0) return false;

            map[x, y - 1] = true;
            return true;
          }
        }

        y++;
      }
    }

    private string DrawMap(bool[,] map, (int x, int y) start, (int x, int y)? sand = null)
    {
      var sb = new StringBuilder();

      var xm = map.GetLength(0);
      var ym = map.GetLength(1);

      for (var x = 0; x < xm; x++)
      {
        sb.Append('-');
      }

      sb.AppendLine();

      for (var y = 0; y < ym; y++)
      {
        for (var x = 0; x < xm; x++)
        {
          if ((x, y) == sand)
          {
            sb.Append('o');
          }
          else if ((x, y) == start)
          {
            sb.Append('+');
          }
          else
          {
            sb.Append(map[x, y] ? '#' : ' ');
          }
        }

        sb.AppendLine();
      }

      for (var x = 0; x < xm; x++)
      {
        sb.Append('-');
      }

      return sb.ToString();
    }

    public override string Task1()
    {
      var (map, start) = GetMap();

      Console.WriteLine(DrawMap(map, start));

      var sand = 0;

      while (DropSand(map, start))
      {
        // Console.WriteLine(DrawMap(map, start));

        sand++;
      }

      return sand.ToString();
    }

    public override string Task2()
    {
      var (map, start) = GetMap(true);

      // Console.WriteLine(DrawMap(map, start));

      var sand = 0;

      while (DropSand(map, start))
      {
        // Console.WriteLine(DrawMap(map, start));

        sand++;
      }

      return sand.ToString();
    }
  }
}
